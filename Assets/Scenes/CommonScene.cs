using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapTap.License;
using TapTap.Common;

public class CommonScene : MonoBehaviour,ITapDlcCallback,ITapLicenseCallback
{
    // Start is called before the first frame update
    void Start()
    {
        TapLicense.SetDLCCallback(this);
        TapLicense.SetLicenseCallBack(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLicenseSuccess()
    {
        label = "许可成功";
    }

    public void OnQueryCallBack(TapLicenseQueryCode code, Dictionary<string, object> queryList)
    {
        //string str = Json.Serialize(queryList);
        var txt = "code:" + code;
        foreach (var item in queryList)
        {
            txt = txt + " key:" + item.Key + " value:" + item.Value;
        }
        label = txt;
    }

    public void OnOrderCallBack(string sku, TapLicensePurchasedCode status)
    {
        label = "sku:" + sku + "\n" + "status:" + status;
    }

    private string text = "28,30";

    private string purchaseText = "28";

    private string appID = "7133";
    
    private string label;
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 35;
        text = GUI.TextArea(new Rect(60, 140, 400, 80), text, inputStyle);
        
        purchaseText = GUI.TextArea(new Rect(500, 140, 360, 80), purchaseText, inputStyle);
        
        var labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 30
        };
        GUI.Label(new Rect(400, 850, 550, 1300), label, labelStyle);

        if (GUI.Button(new Rect(60, 260, 220, 80), "查询DLC", style))
        {
            string[] ss = text.Split(',');
            TapLicense.QueryDLC(ss);
        }

        if (GUI.Button(new Rect(330, 260, 220, 80), "购买DLC", style))
        {
            TapLicense.PurchaseDLC(purchaseText);
        }

        if (GUI.Button(new Rect(600, 260, 350, 80), "检查游戏是否购买", style))
        {
            TapLicense.Check();
        }

        if (GUI.Button(new Rect(60, 380, 300, 80), "是否安装Tap", style))
        {
            TapCommon.IsTapTapInstalled((isInstalled) =>
            {
                label = isInstalled ? "安装TapTap登录" : "未安装TapTap登录";
            });
        }

        if (GUI.Button(new Rect(410, 380, 400, 80), "是否安装Tap国际版", style))
        {
            TapCommon.IsTapTapGlobalInstalled((isInstalled) =>
            {
                label = isInstalled ? "安装Tap国际版" : "未安装Tap国际版";
            });
        }

        if (GUI.Button(new Rect(60, 500, 300, 80), "Tap中更新游戏", style))
        {
            TapCommon.UpdateGameInTapTap(appID, (isUpdate) =>
            {
                label = isUpdate ? "在Tap中更新游戏" : "未在Tap中更新游戏";
            });
        }

        if (GUI.Button(new Rect(410, 500, 430, 80), "Tap国际版中更新游戏", style))
        {
            TapCommon.UpdateGameInTapGlobal(appID, (isUpdate) =>
            {
                label = isUpdate ? "在国际版Tap中更新游戏" : "未在国际版Tap中更新游戏";
            });
        }

        if (GUI.Button(new Rect(60, 620, 300, 80), "Tap中打开页面", style))
        {
            TapCommon.OpenReviewInTapTap(appID, (isOpen) =>
            {
                label = isOpen ? "在Tap中打开页面" : "未在Tap中打开页面";
            });
        }

        if (GUI.Button(new Rect(410, 620, 430, 80), "Tap国际版中打开页面", style))
        {
            TapCommon.openReviewInTapGlobal(appID, (isOpen) =>
            {
                label = isOpen ? "在国际版Tap中打开页面" : "未在国际版Tap中打开页面";
            });
        }

        //待定
        // if (GUI.Button(new Rect(60, 750, 240, 80), "是否国内", style))
        // {
        //     TapCommon.GetRegionCode((isMaind) =>
        //     {
        //         label = isMaind ? "在国内" : "在国外";
        //     });
        // }

        if (GUI.Button(new Rect(60, 750, 160, 80), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}




























