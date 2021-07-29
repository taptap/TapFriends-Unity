using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TapTap.Common;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using Random = System.Random;

namespace TapTap.Login.Internal
{
    internal class QRCodeWindow : UIElement
    {
        // \ue904\ue909\ue918\ue91b  点击刷新
        private static readonly string TEXT_CLICK_TO_REFRESH = "";
        // \ue90f\ue91c\ue911\ue91a\ue901\ue902\ue903\ue90c  您已取消此次登录
        private static readonly string TEXT_CANCEL_LOGIN = "";
        // \ue910\ue91f\ue91b\ue914\ue90d  请重新扫码
        private static readonly string TEXT_PLEASE_RESCRAN = "";
        // \ue914\ue90d\ue900\ue907  扫码成功
        private static readonly string TEXT_SCAN_SUCCESS = "";
        // \ue910\ue91e\ue917\ue90a\ue915\ue912\ue913 请在手机上确认
        private static readonly string TEXT_CONFIRM_ON_PHONE = "";

        public RawImage QRCodeRawImage;
        public Text StatusText;
        public Text SubStatusText;
        public Image RefreshImage;
        public Button RefreshButton;
        public Button CloseButton;
        public Button LogoButton;
        public Button Notice2Button;
        public Image NoticeImage;


        private string clientId;

        private string deviceCode;
        private long expireAt = 0;
        private long lastCheckAt = 0;
        private long interval = 5;

        private Net net;

        public override Dictionary<string, object> Extra
        {
            get
            {
                return extra;
            }
            set
            {
                extra = value;
                if (extra != null)
                {
                    if (extra.ContainsKey("client_id"))
                    {
                        clientId = extra["client_id"] as string;
                    }
                }
            }
        }

        void Awake()
        {
            GameObject qrImageObject = transform.Find("QRImage").gameObject;
            QRCodeRawImage = qrImageObject.GetComponent<RawImage>();
            GameObject statusObject = transform.Find("Status").gameObject;
            StatusText = statusObject.GetComponent<Text>();
            GameObject subStatusObject = transform.Find("SubStatus").gameObject;
            SubStatusText = subStatusObject.GetComponent<Text>();
            GameObject refreshObject = transform.Find("Refresh").gameObject;
            RefreshImage = refreshObject.GetComponent<Image>();
            GameObject refreshButtonObject = refreshObject.transform.Find("RefreshButton").gameObject;
            RefreshButton = refreshButtonObject.GetComponent<Button>();
            GameObject closeObject = transform.Find("Close").gameObject;
            CloseButton = closeObject.GetComponent<Button>();
            GameObject noticeGameObject = transform.Find("Notice").gameObject;
            NoticeImage = noticeGameObject.GetComponent<Image>();
            LogoButton = noticeGameObject.transform.Find("Logo").gameObject.GetComponent<Button>();
            Notice2Button = noticeGameObject.transform.Find("Notice2").gameObject.GetComponent<Button>();


            RefreshImage.gameObject.SetActive(false);
            RefreshButton.onClick.AddListener(RefreshCode);
            LogoButton.onClick.AddListener(GoToTapTapPage);
            Notice2Button.onClick.AddListener(GoToTapTapPage);
            SubStatusText.gameObject.SetActive(false);
            StatusText.gameObject.SetActive(false);
            CloseButton.onClick.AddListener(Close);

        }

        void Update()
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();

            gameObject.AddComponent<Net>();
            net = gameObject.GetComponent<Net>();
            GetCode();
        }

        public override void OnExit()
        {
            base.OnExit();
            Destroy(net.gameObject);
        }

        void Close()
        {
            OnCallback(UIManager.RESULT_CLOSE, "Close button clicked.");
            GetUIManager().Pop();
        }

        void GoToTapTapPage()
        {
            Application.OpenURL("https://l.tapdb.net/CEi1gsDg");
        }

        void RefreshCode()
        {
            StatusText.gameObject.SetActive(false);
            SubStatusText.gameObject.SetActive(false);
            NoticeImage.gameObject.SetActive(true);
            GetCode();
        }

        void GetProfile(AccessToken accessToken, int timestamp = 0)
        {
            string url = "https://openapi.taptap.com/account/profile/v1?client_id=" + clientId;
            int ts = timestamp;
            if (ts == 0)
            {
                var dt = DateTime.UtcNow - new DateTime(1970, 1, 1);
                ts = (int)dt.TotalSeconds;
            }
            string sign = "MAC " + GetAuthorizationHeader(accessToken.kid,
                accessToken.macKey,
                accessToken.macAlgorithm,
                "GET",
                "/account/profile/v1?client_id=" + clientId,
                "openapi.taptap.com",
                "443", ts);
            net.GetAsync(url,
                sign,
                null,
                (string result) =>
                {
                    try
                    {
                        Dictionary<string, string> tokenDict = new Dictionary<string, string>();
                        tokenDict.Add("kid", accessToken.kid);
                        tokenDict.Add("mac_key", accessToken.macKey);
                        tokenDict.Add("access_token", accessToken.accessToken);
                        tokenDict.Add("token_type", accessToken.tokenType);
                        tokenDict.Add("mac_algorithm", accessToken.macAlgorithm);

                        Dictionary<string, object> resultDict = Json.Deserialize(result) as Dictionary<string, object>;
                        if(resultDict.ContainsKey("success") && (bool)resultDict["success"])
                        {
                            Dictionary<string, object> dataDict = resultDict["data"] as Dictionary<string, object>;
                            DataStorage.SaveString("taptapsdk_accesstoken", Json.Serialize(tokenDict));
                            DataStorage.SaveString("taptapsdk_profile", Json.Serialize(dataDict));
                            OnCallback(UIManager.RESULT_SUCCESS, accessToken);
                            GetUIManager().Pop();
                        }
                        else
                        {
                            OnCallback(UIManager.RESULT_FAILED, "Get profile error");
                            GetUIManager().Pop();
                        }
     
                    } catch (Exception e)
                    {
                        OnCallback(UIManager.RESULT_FAILED, "Get profile error");
                        GetUIManager().Pop();
                    }
                },
                (int error, string msg) =>
                {
                    if (timestamp == 0 && !string.IsNullOrEmpty(msg) && msg.Contains("invalid_time") && msg.Contains("now"))
                    {
                        try
                        {
                            var json = Json.Deserialize(msg) as Dictionary<string, object>;
                            var now = (int) (long)json["now"];
                            GetProfile(accessToken, now);
                            return;
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }
                    }
                    OnCallback(UIManager.RESULT_FAILED, msg);
                    GetUIManager().Pop();
                });

  
        }

        public string GetAuthorizationHeader(string kid,
            string macKey,
            string macAlgorithm,
            string method,
            string uri,
            string host,
            string port,
            int timestamp)
        {
            string nonce = new Random().Next().ToString();

            string normalizedString = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n\n",
                                                    timestamp,
                                                    nonce,
                                                    method,
                                                    uri,
                                                    host,
                                                    port);

            HashAlgorithm hashGenerator = null;
            if (macAlgorithm == "hmac-sha-256")
            {
                hashGenerator = new HMACSHA256(Encoding.ASCII.GetBytes(macKey));
            }
            else if (macAlgorithm == "hmac-sha-1")
            {
                hashGenerator = new HMACSHA1(Encoding.ASCII.GetBytes(macKey));
            }
            else
            {
                throw new InvalidOperationException("Unsupported MAC algorithm");
            }

            string hash = Convert.ToBase64String(hashGenerator.ComputeHash(Encoding.ASCII.GetBytes(normalizedString)));

            StringBuilder authorizationHeader = new StringBuilder();
            authorizationHeader.AppendFormat(@"id=""{0}"",ts=""{1}"",nonce=""{2}"",mac=""{3}""",
                                             kid, timestamp, nonce, hash);

            return authorizationHeader.ToString();
        }

        void GetCode()
        {
            RefreshImage.gameObject.SetActive(false);
      

            Dictionary<string, string> formParams = new Dictionary<string, string>();
            formParams.Add("client_id", clientId);
            formParams.Add("response_type", "device_code");
            formParams.Add("scope", "public_profile");
            formParams.Add("version", TapTapSdk.VERSION);
            formParams.Add("platform", "unity");
            formParams.Add("info", "{\"device_id\":\"" + SystemInfo.deviceModel + "\"}");
            net.PostAsync("https://www.taptap.com/oauth2/v1/device/code",
                null,
                formParams,
                (string result) =>
                {
                    Dictionary<string, object> resultDict = Json.Deserialize(result) as Dictionary<string, object>;
                    if (resultDict.ContainsKey("success") && (bool)resultDict["success"])
                    {
                        Dictionary<string, object> dataDict = resultDict["data"] as Dictionary<string, object>;
                        if (dataDict.ContainsKey("qrcode_url"))
                        {
                            string qrCodeUrl = dataDict["qrcode_url"] as string;
                            EncodeQRImage(qrCodeUrl, 309, 309);
                            if (dataDict.ContainsKey("device_code"))
                            {
                                deviceCode = dataDict["device_code"] as string;
                            }
                            if (dataDict.ContainsKey("expires_in"))
                            {
                                long expireIn = (long)dataDict["expires_in"];
                                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                expireAt = Convert.ToInt64(ts.TotalSeconds) + expireIn;
                            }
                            if (dataDict.ContainsKey("interval"))
                            {
                                interval = (long)dataDict["interval"];
                            }
                            StartCheck();
                            Debug.Log("QRCODE Get");
                        }
                        else
                        {
                            //StatusText.text = "获取二维码失败";
                            RefreshImage.gameObject.SetActive(true);
                            Debug.Log("QRCODE Get Fail 1");
                        }
                    } else
                    {
                        //StatusText.text = "获取二维码失败";
                        RefreshImage.gameObject.SetActive(true);
                        Debug.Log("QRCODE Get Fail 2");
                    }

                },
                (int error, string msg) =>
                {
                    //StatusText.text = "获取二维码失败";
                    RefreshImage.gameObject.SetActive(true);
                    Debug.Log("QRCODE Get Fail 3");
                }
            );
        }

        void Check()
        {


        }


        void EncodeQRImage(string content, int width, int height)
        {
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            //hints.Add(EncodeHintType.MARGIN, 0);
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M);
            BitMatrix bitMatrix = writer.encode(content, BarcodeFormat.QR_CODE, width, height, hints);
            bitMatrix = DeleteWhite(bitMatrix);
            int w = bitMatrix.Width;
            int h = bitMatrix.Height;
            Texture2D texture = new Texture2D(w, h);
            for (int x = 0; x < h; x++)
            {
                for (int y = 0; y < w; y++)
                {
                    if (bitMatrix[x, y])
                    {
                        texture.SetPixel(y, x, Color.black);
                    }
                    else
                    {
                        texture.SetPixel(y, x, Color.white);
                    }
                }
            }

            texture.Apply();
            texture.filterMode = FilterMode.Point;
            QRCodeRawImage.texture = texture;
        }

        private static BitMatrix DeleteWhite(BitMatrix matrix)
        {
            int[] rec = matrix.getEnclosingRectangle();
            int resWidth = rec[2];
            int resHeight = rec[3];

            BitMatrix resMatrix = new BitMatrix(resWidth, resHeight);
            resMatrix.clear();
            for (int i = 0; i < resWidth; i++)
            {
                for (int j = 0; j < resHeight; j++)
                {
                    if (matrix[i + rec[0], j + rec[1]])
                        resMatrix.flip(i, j);
                }
            }
            return resMatrix;
        }

        void StartCheck()
        {
            StartCoroutine("AutoCheck");
            //Debug.Log("StartCheck");
        }

        void StopCheck()
        {
            StopCoroutine("AutoCheck");
            //Debug.Log("StopCheck");
        }

        private IEnumerator AutoCheck()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                long now = Convert.ToInt64(ts.TotalSeconds);
                //Debug.Log("Now: " + now + "  ExpireAt: " + expireAt + "  Remain: " + (expireAt - now));
                if (now > expireAt)
                {
                    RefreshImage.gameObject.SetActive(true);
                    SubStatusText.gameObject.SetActive(false);
                    StatusText.gameObject.SetActive(false);
                    NoticeImage.gameObject.SetActive(true);
                    StopCheck();
                    break;
                }
                if (now > lastCheckAt + interval)
                {
                    bool wait = true;
                    bool stop = false;
                    Dictionary<string, string> formParams = new Dictionary<string, string>();
                    formParams.Add("grant_type", "device_token");
                    formParams.Add("client_id", clientId);
                    formParams.Add("secret_type", "hmac-sha-1");
                    formParams.Add("code", deviceCode);
                    formParams.Add("version", "1.0");
                    formParams.Add("platform", "unity");
                    formParams.Add("info", "{\"device_id\":\"" + SystemInfo.deviceModel + "\"}");
                    //Debug.Log("Check");
                    net.PostAsync("https://www.taptap.com/oauth2/v1/token",
                        null,
                        formParams,
                        (string result) =>
                        {
                            Dictionary<string, object> resultDict = Json.Deserialize(result) as Dictionary<string, object>;
                            //StatusText.text = "";
                            
                            try
                            {
                                if (resultDict.ContainsKey("success") && (bool)resultDict["success"])
                                {
                                    //StatusText.text = "扫码成功";
                                    //SubStatusText.gameObject.SetActive(false);
                                    Dictionary<string, object> dataDict = resultDict["data"] as Dictionary<string, object>;
                                    AccessToken token = new AccessToken();
                                    token.kid = dataDict["kid"] as string;
                                    token.macKey = dataDict["mac_key"] as string;
                                    token.accessToken = dataDict["access_token"] as string;
                                    token.tokenType = dataDict["token_type"] as string;
                                    token.macAlgorithm = dataDict["mac_algorithm"] as string;

                                    GetProfile(token);

                                }
                                else
                                {
                                    //StatusText.text = "扫描二维码失败";
                                    RefreshImage.gameObject.SetActive(true);
                                    SubStatusText.gameObject.SetActive(false);
                                    StatusText.gameObject.SetActive(false);
                                    NoticeImage.gameObject.SetActive(true);
                                    RefreshImage.gameObject.SetActive(true);
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e);
                                //StatusText.text = "二维码状态异常";
                                RefreshImage.gameObject.SetActive(true);
                                SubStatusText.gameObject.SetActive(false);
                                StatusText.gameObject.SetActive(false);
                                NoticeImage.gameObject.SetActive(true);
                                RefreshImage.gameObject.SetActive(true);
                            }
                            wait = false;
                            stop = true;
                            StopCheck();
                        },
                        (int error, string msg) =>
                        {
                            wait = false;
                            try
                            {
                                Dictionary<string, object> resultDict = Json.Deserialize(msg) as Dictionary<string, object>;
                                if (resultDict.ContainsKey("data"))
                                {
                                    Dictionary<string, object> dataDict = resultDict["data"] as Dictionary<string, object>;
                                    if (dataDict.ContainsKey("error"))
                                    {
                                        string errorMsg = dataDict["error"] as string;
                                        //Debug.Log(errorMsg);
                                        if ("authorization_pending".Equals(errorMsg))
                                        {
                                            //StatusText.text = "扫码二维码登录";
                                        }
                                        else if ("authorization_waiting".Equals(errorMsg))
                                        {
                                            StatusText.text = TEXT_SCAN_SUCCESS;
                                            SubStatusText.text = TEXT_CONFIRM_ON_PHONE;
                                            StatusText.gameObject.SetActive(true);
                                            SubStatusText.gameObject.SetActive(true);
                                            NoticeImage.gameObject.SetActive(false);
                                        }
                                        else if ("access_denied".Equals(errorMsg))
                                        {
                                            StatusText.text = TEXT_CANCEL_LOGIN;
                                            SubStatusText.text = TEXT_PLEASE_RESCRAN;
                                            StatusText.gameObject.SetActive(true);
                                            SubStatusText.gameObject.SetActive(true);
                                            NoticeImage.gameObject.SetActive(false);
                                            stop = true;
                                            StopCheck();
                                            GetCode();
                                            Debug.Log("cancel");
                                        }
                                        else if ("invalid_grant".Equals(errorMsg))
                                        {
                                            RefreshImage.gameObject.SetActive(true);
                                            StatusText.gameObject.SetActive(false);
                                            SubStatusText.gameObject.SetActive(false);
                                            NoticeImage.gameObject.SetActive(true);
                                            stop = true;
                                            StopCheck();
                                        }
                                        else if ("slow_down".Equals(errorMsg))
                                        {
                                        }
                                        else
                                        {
                                            RefreshImage.gameObject.SetActive(true);
                                            SubStatusText.gameObject.SetActive(false);
                                            StatusText.gameObject.SetActive(false);
                                            NoticeImage.gameObject.SetActive(true);
                                            stop = true;
                                            StopCheck();
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e);
                                RefreshImage.gameObject.SetActive(true);
                                SubStatusText.gameObject.SetActive(false);
                                StatusText.gameObject.SetActive(false);
                                NoticeImage.gameObject.SetActive(true);
                                stop = true;
                                StopCheck();
                            }

                        }
                    );
                    while (wait)
                    {
                        yield return new WaitForSeconds(0.5f);
                    }
                    ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    lastCheckAt = Convert.ToInt64(ts.TotalSeconds);
                    if (stop) break;
                }

            }
        }

    }
}
