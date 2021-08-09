using System.Collections;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine;
using UnityEngine.UI;
using TapTap.License;
using TapTap.TapDB;
using UnityNative.Toasts.Example;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update

    //tapdb
    public Button tapdb;
    //动态
    public Button moment;
    //登录
    public Button login;
    //国内
    public Button inland;
    //海外
    public Button overseas;
    //rnd-cn
    public Button rnd_cn;
    //rnd-io
    public Button rnd_io;
    //TapFriend
    public Button tapFriend;
    //common
    public Button common;
    //设置语言
    public Button setLanguage;
    //tapdb开关
    public Toggle tapdbSwitch;
    //idfa开关
    public Toggle idfaSwitch;
    //channelField
    public InputField channelField;
    //gameVersionField
    public InputField gameVersionField;
    //languageField
    public InputField languageField;

    public Button achievement;
    
    void Start()
    {
        // Judge.JudgeDeviceModel();
        tapdb.onClick.AddListener(OnTapdbClicked);
        moment.onClick.AddListener(OnMomentClicked);
        login.onClick.AddListener(OnLoginClicked);
        inland.onClick.AddListener(OnInlandClicked);
        overseas.onClick.AddListener(OnOverseasClicked);
        rnd_cn.onClick.AddListener(OnRnd_cnClicked);
        rnd_io.onClick.AddListener(OnRnd_ioClicked);
        tapFriend.onClick.AddListener(OnTapfriendClicked);
        common.onClick.AddListener(OnCommonClicked);
        setLanguage.onClick.AddListener(OnSetLanguageClicked);
        
        channelField.onEndEdit.AddListener(OnChannelInput);
        gameVersionField.onEndEdit.AddListener(OnGameVerisonInput);
        languageField.onEndEdit.AddListener(OnLanguageInput);
        
        tapdbSwitch.onValueChanged.AddListener(OnTapdbSwitch);
        idfaSwitch.onValueChanged.AddListener(OnIdfaSwitch);
        achievement.onClick.AddListener(OnAchievementClick);
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

    private void OnAchievementClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(5);
    }
    
    private void OnTapdbClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
    }
    
    private void OnMomentClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
    }
    
    private void OnLoginClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    
    private void OnInlandClicked()
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
    
    private void OnOverseasClicked()
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
    
    private void OnRnd_cnClicked()
    {
        TapCommon.AddHost("https://openapi.taptap.com/", "https://open.api.xdrnd.com/");
        TapCommon.AddHost("https://www.taptap.com/", "https://www.xdrnd.com/");
        TapCommon.AddHost("https://tds-moment.taptap.com/", "https://tds-moment.xdrnd.com/");
        TapCommon.AddHost("https://tds-moment.taptap-api.com/", "https://tds-moment.api.xdrnd.com/");
        TapCommon.AddHost("https://tds-tapsdk.cn.tapapis.com/achievement/api/v1/clients/", "https://tds-api.xdrnd.com/achievement/api/v1/clients/");

        var config = new TapConfig.Builder()
            .ClientID("uZ8Yy6cSXVOR6AMRPj")
            .ClientToken("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
            .ServerURL("https://api.leancloud.cn")
            .RegionType(RegionType.CN)
            .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
            .ConfigBuilder();

        TapDB.AdvertiserIDCollectionEnabled(isIDFA);

        TapBootstrap.Init(config);

    }
    
    private void OnRnd_ioClicked()
    {
        
        TapCommon.AddHost("https://openapi.tap.io/", "https://open.api.xdrnd.com/");
        TapCommon.AddHost("https://www.tap.io/", "https://www.xdrnd.com/");
        TapCommon.AddHost("https://tds-moment.tap.io/", "https://tds-moment-io.xdrnd.com/");
        TapCommon.AddHost("https://moment.intl.tapapis.com/", "https://tds-moment-io.api.xdrnd.com/");

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
    
    private void OnTapfriendClicked()
    {
    }
    
    private void OnCommonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
    }
    
    private void OnSetLanguageClicked()
    {
        var languageType = int.Parse(language);
        TapCommon.SetLanguage((TapLanguage) languageType);
    }
    
    private void OnChannelInput(string inputInfo)
    {
        channel = inputInfo;
        Debug.Log($"channel值为：{inputInfo} ==");
    }
    
    private void OnGameVerisonInput(string inputInfo)
    {
        gameVersion = inputInfo;
        Debug.Log($"gameVersion值为：{inputInfo} ==");
    }
    
    private void OnLanguageInput(string inputInfo)
    {
        language = inputInfo;
        Debug.Log($"Language值为：{inputInfo} ==");
    }

    private void OnTapdbSwitch(bool value)
    {
        isSwitch = value;
        Debug.Log($"tapdbSwitch: {value} ==");
    }

    private void OnIdfaSwitch(bool value)
    {
        isIDFA = value;
        Debug.Log($"idfaSwitch: {value} ==");
    }

    private string channelValue()
    {
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

    // private void OnGUI()
    // {
        // GUIStyle style = new GUIStyle(GUI.skin.button);
        // style.fontSize = 40;
        //
        // GUIStyle myToggleStyle = new GUIStyle(GUI.skin.toggle)
        // {
        //     fontSize = 35,
        // };
        // GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        // inputStyle.fontSize = 27;
        //
        // isSwitch = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice ? 100 : 0, 200, 55), isSwitch, "TapDB开关",
        //     myToggleStyle);
        //
        // isIDFA = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice ? 200 : 60, 200, 55), isIDFA, "IDFA 开关", myToggleStyle);
        //
        // channel = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 280 : 135, 330, 70), channel, inputStyle);
        //
        // gameVersion = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 360 : 215, 330, 70), gameVersion, inputStyle);
        //
        // language = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice ? 480 : 295, 330, 70), language, inputStyle);

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 100 : 0, 280, 100), "RND-IO", style))
        // {
        //     var config = new TapConfig.Builder()
        //         // .ClientID("uZ8Yy6cSXVOR6AMRPj")
        //         .ClientID("UBm5x5JP7ZGEgRsXY5")
        //         // .ClientToken("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
        //         .ClientToken("hlPXHzw2XHpATDxGD8FD1Rtwu0iFOBfuGY2XFXR5")
        //         .RegionType(RegionType.IO)
        //         .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
        //         .ConfigBuilder();
        //
        //     TapDB.AdvertiserIDCollectionEnabled(isIDFA);
        //
        //     TapBootstrap.Init(config);
        // }

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 250 : 150, 280, 100), "RND-CN", style))
        // {
        //     var config = new TapConfig.Builder()
        //         .ClientID("uZ8Yy6cSXVOR6AMRPj")
        //         .ClientToken("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
        //         .RegionType(RegionType.CN)
        //         .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
        //         .ConfigBuilder();
        //
        //     TapDB.AdvertiserIDCollectionEnabled(isIDFA);
        //
        //     TapBootstrap.Init(config);
        // }

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 400 : 300, 280, 100), "海外", style))
        // {
        //     var config = new TapConfig.Builder()
        //         .ClientID("KFV9Pm9ojdmWkkRJeb")
        //         .ClientToken("7mpVJdXIOLQxvQdqjEEpiz7eLf82cMwYkdgoAZqF")
        //         .RegionType(RegionType.IO)
        //         .EnableTapDB(isSwitch)
        //         .ConfigBuilder();
        //
        //     TapBootstrap.Init(config);
        // }

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 550 : 450, 280, 100), "国内", style))
        // {
        //     var config = new TapConfig.Builder()
        //         .ClientID("0RiAlMny7jiz086FaU")
        //         .ClientToken("8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU")
        //         .RegionType(RegionType.CN)
        //         .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
        //         .ConfigBuilder();
        //     TapBootstrap.Init(config);
        // }

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 700 : 600, 280, 100), "登陆", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        // }

        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 850 : 750, 280, 100), "动态", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        // }
        //
        // if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1000 : 900, 280, 100), "TapDB", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        // }

        // if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 570 : 425, 280, 100), "TapFriend", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
        // }
        //
        // if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 710 : 565, 280, 100), "Common", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(5);
        // }
        //
        // if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice ? 850 : 715, 280, 100), "设置语言", style))
        // {
        //     var languageType = int.Parse(language);
        //     TapBootstrap.SetPreferLanguage((TapLanguage) languageType);
        // }
    // }
}