using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapTap.License;
using TapTap.Common;
using UnityEngine.UI;

public class CommonScene : MonoBehaviour, ITapDlcCallback, ITapLicenseCallback
{
    // Start is called before the first frame update

    //查询DLC
    public Button queryDLC;

    //购买DLC
    public Button purchaseDLC;

    //检查游戏是否购买
    public Button checkWhetherPurchase;

    //是否安装Tap
    public Button whetherInstallTap;

    //是否安装Tap国际版
    public Button whetherInstallInternationalTap;

    //Tap中更新游戏
    public Button updateInGame;

    //Tap国际版中更新游戏
    public Button updateInInternationalGame;

    //Tap中打开页面
    public Button openPage;

    //Tap国际版中打开页面
    public Button openPageInInternational;

    //返回
    public Button back;

    //查询DLCinput
    public InputField queryDLCField;

    //购买DLCinput
    public InputField purchaseDLCField;

    //文本信息
    public Text label;

    public Button UpdateInTap, UpdateInTapG, UpdateUrlInTap, UpdateUrlInTapG, OpenInTap, OpenInTapG;

    void Start()
    {
        TapLicense.SetDLCCallback(this);
        TapLicense.SetLicenseCallBack(this);

        queryDLC.onClick.AddListener(onQueryDLCClicked);
        purchaseDLC.onClick.AddListener(onPurchaseDLCClicked);
        checkWhetherPurchase.onClick.AddListener(onCheckWhetherPurchaseClicked);
        whetherInstallTap.onClick.AddListener(onWhetherInstallTapClicked);
        whetherInstallInternationalTap.onClick.AddListener(onWhetherInstallInternationalTapClicked);
        updateInGame.onClick.AddListener(onUpdateInGameClicked);
        updateInInternationalGame.onClick.AddListener(onUpdateInInternationalGameClicked);
        openPage.onClick.AddListener(onOpenPageClicked);
        openPageInInternational.onClick.AddListener(onOpenPageInInternationalClicked);
        back.onClick.AddListener(onBackClicked);

        queryDLCField.onEndEdit.AddListener(onQueryDLCField);
        purchaseDLCField.onEndEdit.AddListener(onPurchaseDLCField);

        UpdateInTap.onClick.AddListener(async () =>
        {
            label.text = $"update in Tap result:{await TapCommon.UpdateGameAndFailToWebInTapTap("197372")}";
        });

        UpdateInTapG.onClick.AddListener((async () =>
        {
            label.text = $"update in TapG Result:{await TapCommon.UpdateGameAndFailToWebInTapGlobal("197372")}";
        }));

        OpenInTap.onClick.AddListener(async () =>
        {
            label.text = $"open tap download url result:{await TapCommon.OpenWebDownloadUrlOfTapTap("197372")}";
        });

        OpenInTapG.onClick.AddListener(async () =>
        {
            label.text = $"open tapg download url result:{await TapCommon.OpenWebDownloadUrlOfTapGlobal("197372")}";
        });

        UpdateUrlInTap.onClick.AddListener(async () =>
        {
            label.text =
                $"open tap url result:{await TapCommon.UpdateGameAndFailToWebInTapTap("197372", "https://www.baidu.com/")}";
        });

        UpdateUrlInTapG.onClick.AddListener(async () =>
        {
            label.text =
                $"open tap url result:{await TapCommon.UpdateGameAndFailToWebInTapGlobal("197372", "https://www.baidu.com/")}";
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void onQueryDLCClicked()
    {
        string[] ss = text.Split(',');
        TapLicense.QueryDLC(ss);
    }

    private void onPurchaseDLCClicked()
    {
        TapLicense.PurchaseDLC(purchaseText);
    }

    private void onCheckWhetherPurchaseClicked()
    {
        TapLicense.Check();
    }

    private void onWhetherInstallTapClicked()
    {
        TapCommon.IsTapTapInstalled((isInstalled) => { label.text = isInstalled ? "安装TapTap登录" : "未安装TapTap登录"; });
    }

    private void onWhetherInstallInternationalTapClicked()
    {
        TapCommon.IsTapTapGlobalInstalled((isInstalled) => { label.text = isInstalled ? "安装Tap国际版" : "未安装Tap国际版"; });
    }

    private void onUpdateInGameClicked()
    {
        TapCommon.UpdateGameInTapTap(appID, (isUpdate) => { label.text = isUpdate ? "在Tap中更新游戏" : "未在Tap中更新游戏"; });
    }

    private void onUpdateInInternationalGameClicked()
    {
        TapCommon.UpdateGameInTapGlobal(appID,
            (isUpdate) => { label.text = isUpdate ? "在国际版Tap中更新游戏" : "未在国际版Tap中更新游戏"; });
    }

    private void onOpenPageClicked()
    {
        TapCommon.OpenReviewInTapTap(appID, (isOpen) => { label.text = isOpen ? "在Tap中打开页面" : "未在Tap中打开页面"; });
    }

    private void onOpenPageInInternationalClicked()
    {
        TapCommon.OpenReviewInTapGlobal(appID, (isOpen) => { label.text = isOpen ? "在国际版Tap中打开页面" : "未在国际版Tap中打开页面"; });
    }

    private void onBackClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    private void onQueryDLCField(string input)
    {
        text = input;
        Debug.Log($"QueryDLCField: {input} ==");
    }

    private void onPurchaseDLCField(string input)
    {
        purchaseText = input;
        Debug.Log($"PurchaseDLCField: {input} ==");
    }

    public void OnLicenseSuccess()
    {
        label.text = "许可成功";
    }

    public void OnQueryCallBack(TapLicenseQueryCode code, Dictionary<string, object> queryList)
    {
        //string str = Json.Serialize(queryList);
        var txt = "code:" + code;
        foreach (var item in queryList)
        {
            txt = txt + " key:" + item.Key + " value:" + item.Value;
        }

        label.text = txt;
    }

    public void OnOrderCallBack(string sku, TapLicensePurchasedCode status)
    {
        label.text = "sku:" + sku + "\n" + "status:" + status;
    }

    private string text = "28,30";

    private string purchaseText = "28";

    private string appID = "7133";

    // private void OnGUI()
    // {
    // GUIStyle style = new GUIStyle(GUI.skin.button);
    // style.fontSize = 40;
    //
    // GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
    // inputStyle.fontSize = 35;
    // text = GUI.TextArea(new Rect(60, 140, 400, 80), text, inputStyle);
    //
    // purchaseText = GUI.TextArea(new Rect(500, 140, 360, 80), purchaseText, inputStyle);
    //
    // var labelStyle = new GUIStyle(GUI.skin.label)
    // {
    //     fontSize = 30
    // };
    // GUI.Label(new Rect(400, 850, 550, 1300), label, labelStyle);

    // if (GUI.Button(new Rect(60, 260, 220, 80), "查询DLC", style))
    // {
    //     string[] ss = text.Split(',');
    //     TapLicense.QueryDLC(ss);
    // }

    // if (GUI.Button(new Rect(330, 260, 220, 80), "购买DLC", style))
    // {
    //     TapLicense.PurchaseDLC(purchaseText);
    // }

    // if (GUI.Button(new Rect(600, 260, 350, 80), "检查游戏是否购买", style))
    // {
    //     TapLicense.Check();
    // }

    // if (GUI.Button(new Rect(60, 380, 300, 80), "是否安装Tap", style))
    // {
    //     TapCommon.IsTapTapInstalled((isInstalled) => { label = isInstalled ? "安装TapTap登录" : "未安装TapTap登录"; });
    // }

    // if (GUI.Button(new Rect(410, 380, 400, 80), "是否安装Tap国际版", style))
    // {
    //     TapCommon.IsTapTapGlobalInstalled((isInstalled) => { label = isInstalled ? "安装Tap国际版" : "未安装Tap国际版"; });
    // }

    // if (GUI.Button(new Rect(60, 500, 300, 80), "Tap中更新游戏", style))
    // {
    //     TapCommon.UpdateGameInTapTap(appID, (isUpdate) => { label = isUpdate ? "在Tap中更新游戏" : "未在Tap中更新游戏"; });
    // }

    // if (GUI.Button(new Rect(410, 500, 430, 80), "Tap国际版中更新游戏", style))
    // {
    //     TapCommon.UpdateGameInTapGlobal(appID,
    //         (isUpdate) => { label = isUpdate ? "在国际版Tap中更新游戏" : "未在国际版Tap中更新游戏"; });
    // }

    // if (GUI.Button(new Rect(60, 620, 300, 80), "Tap中打开页面", style))
    // {
    //     TapCommon.OpenReviewInTapTap(appID, (isOpen) => { label = isOpen ? "在Tap中打开页面" : "未在Tap中打开页面"; });
    // }

    // if (GUI.Button(new Rect(410, 620, 430, 80), "Tap国际版中打开页面", style))
    // {
    //     TapCommon.OpenReviewInTapGlobal(appID, (isOpen) => { label = isOpen ? "在国际版Tap中打开页面" : "未在国际版Tap中打开页面"; });
    // }

    //待定
    // if (GUI.Button(new Rect(60, 750, 240, 80), "是否国内", style))
    // {
    //     TapCommon.GetRegionCode((isMaind) =>
    //     {
    //         label = isMaind ? "在国内" : "在国外";
    //     });
    // }

    // if (GUI.Button(new Rect(60, 750, 160, 80), "返回", style))
    // {
    //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    // }
    // }
}