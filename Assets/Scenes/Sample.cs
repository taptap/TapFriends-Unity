using System.Collections;
using TapTap.Bootstrap;
using UnityEngine;
using UnityEngine.UI;
using TapTap.TapDB;
using JudgeDevice;
using LeanCloud;
using TapTap.Common;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update

    private Toggle switchButton;

    void Start()
    {
        Judge.JudgeDeviceModel();
        LCLogger.LogDelegate = (level, message) =>
        {
            switch (level)
            {
                case LCLogLevel.Debug:
                    Debug.Log($"[DEBUG] {message}");
                    break;
                case LCLogLevel.Warn:
                    Debug.LogWarning($"[WARN] {message}");
                    break;
                case LCLogLevel.Error:
                    Debug.LogError($"[ERROR] {message}");
                    break;
                default:
                    break;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool isSwitch = true;
    private bool isIDFA = true;
    private string channel = "输入TapDB：channel";
    private string gameVersion = "输入TapDB：gameVersion";
    private string language = "0";

    private string channelValue()
    {
        Hashtable ht = new Hashtable();
        if (channel == "输入TapDB：channel")
        {
            return "";
        }

        return channel;
    }

    private string gameVersionValue()
    {
        if (gameVersion == "输入TapDB：gameVersion")
        {
            return "";
        }

        return gameVersion;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        GUIStyle myToggleStyle = new GUIStyle(GUI.skin.toggle)
        {
            fontSize = 35,
        };
        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 27;

        isSwitch = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice ? 100 : 0, 200, 55), isSwitch, "TapDB开关",
            myToggleStyle);

        isIDFA = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice ? 200 : 60, 200, 55), isIDFA, "IDFA 开关", myToggleStyle);

        channel = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 280 : 135, 330, 70), channel, inputStyle);

        gameVersion = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 360 : 215, 330, 70), gameVersion, inputStyle);

        language = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 480 : 295, 330, 70), language, inputStyle);

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 100 : 0, 280, 100), "RND-IO", style))
        {
            var config = new TapConfig.Builder()
                // .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientID("UBm5x5JP7ZGEgRsXY5")
                // .ClientToken("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .ClientToken("hlPXHzw2XHpATDxGD8FD1Rtwu0iFOBfuGY2XFXR5")
                .ServerURL("https://ikggdre2.lc-cn-n1-shared.com")
                .RegionType(RegionType.IO)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
                .ConfigBuilder();

            TapDB.AdvertiserIDCollectionEnabled(isIDFA);

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 250 : 150, 280, 100), "RND-CN", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("WsExTi2nldTGyerBiv")
                .ClientToken("ooKXC7B208wxhHhWtnRqRkrCMXiD80E4xz8kveWG")
                .ServerURL("https://api.leancloud.cn")
                .RegionType(RegionType.CN)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
                .ConfigBuilder();

            TapDB.AdvertiserIDCollectionEnabled(isIDFA);

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 400 : 300, 280, 100), "海外", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("KFV9Pm9ojdmWkkRJeb")
                .ClientToken("7mpVJdXIOLQxvQdqjEEpiz7eLf82cMwYkdgoAZqF")
                .RegionType(RegionType.IO)
                .ServerURL("https://ikggdre2.lc-cn-n1-shared.com")
                .EnableTapDB(isSwitch)
                .ConfigBuilder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 550 : 450, 280, 100), "国内", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("0RiAlMny7jiz086FaU")
                .ClientToken("8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU")
                .ServerURL("https://0rialmny.cloud.tds1.tapapis.cn")
                .RegionType(RegionType.CN)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
                .ConfigBuilder();
            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 700 : 600, 280, 100), "登陆", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 850 : 750, 280, 100), "动态", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1000 : 900, 280, 100), "TapDB", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        }

        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 570 : 425, 280, 100), "TapFriend", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
        }

        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 710 : 565, 280, 100), "Common", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(5);
        }

        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 850 : 715, 280, 100), "设置语言", style))
        {
            var languageType = int.Parse(language);
            TapCommon.SetLanguage((TapLanguage) languageType);
        }
        
        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 990 : 855, 280, 100), "成就", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(6);
        }
    }
}