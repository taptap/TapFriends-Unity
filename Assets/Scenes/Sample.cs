using System;
using System.Collections.Generic;
using TapTap.Bootstrap;
using TapTap.Common;
using TapTap.TapDB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TapTap.Support;
using UnityEngine.Assertions;


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

    private void OnDestroy()
    {
        TapSupport.Pause();
    }

    private bool isSwitch = true;
    private bool isIDFA = true;
    private string channel = "输入TapDB：channel";
    private string gameVersion = "输入TapDB：gameVersion";
    private string language = "0";

    private void InitTapSupport()
    {
        TapSupport.Init("https://ticket.sdjdd.com", "-", new TapSupportCallback
        {
            UnReadStatusChanged = (hasUnRead, exception) =>
            {
                Debug.Log($"hasUnRead:{hasUnRead} exception:{exception}");
            }
        });

        TapSupport.AnonymousLogin(null);

        TapSupport.Resume();

        TestTimer();

        Debug.Log("init TapSupport Success And Start Looper");
    }

    private void OnAchievementClick()
    {
        SceneManager.LoadSceneAsync(5);
    }

    private void OnTapdbClicked()
    {
        SceneManager.LoadSceneAsync(3);
    }

    private void OnMomentClicked()
    {
        SceneManager.LoadSceneAsync(2);
    }

    private void OnLoginClicked()
    {
        SceneManager.LoadSceneAsync(1);
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

        InitTapSupport();
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

        InitTapSupport();
    }

    private void OnRnd_cnClicked()
    {
        if (Platform.IsIOS())
        {
            TapCommon.AddHost("https://openapi.taptap.com", "https://open.api.xdrnd.com");
            TapCommon.AddHost("https://www.taptap.com", "https://www.xdrnd.com");
            TapCommon.AddHost("https://tds-moment.taptap.com/", "https://tds-moment.xdrnd.com/");
            TapCommon.AddHost("https://tds-moment.taptap-api.com", "https://tds-moment.api.xdrnd.com");
            TapCommon.AddHost("https://tds-tapsdk.cn.tapapis.com",
                "https://tds-api.xdrnd.com");
        }
        else
        {
            TapCommon.AddHost("https://openapi.taptap.com/", "https://open.api.xdrnd.com/");
            TapCommon.AddHost("https://www.taptap.com/", "https://www.xdrnd.com/");
            TapCommon.AddHost("https://tds-moment.taptap.com/", "https://tds-moment.xdrnd.com/");
            TapCommon.AddHost("https://tds-moment.taptap-api.com/", "https://tds-moment.api.xdrnd.com/");
            TapCommon.AddHost("https://tds-tapsdk.cn.tapapis.com/achievement/api/v1/clients/",
                "https://tds-api.xdrnd.com/achievement/api/v1/clients/");
        }


        var config = new TapConfig.Builder()
            .ClientID("uZ8Yy6cSXVOR6AMRPj")
            .ClientToken("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
            .ServerURL("https://api.leancloud.cn")
            .RegionType(RegionType.CN)
            .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
            .ConfigBuilder();

        TapDB.AdvertiserIDCollectionEnabled(isIDFA);

        TapBootstrap.Init(config);

        InitTapSupport();
    }

    private void OnRnd_ioClicked()
    {
        if (Platform.IsAndroid())
        {
            TapCommon.AddHost("https://openapi.tap.io/", "https://open.api.xdrnd.com/");
            TapCommon.AddHost("https://www.tap.io/", "https://www.xdrnd.com/");
            TapCommon.AddHost("https://tds-moment.tap.io/", "https://tds-moment-io.xdrnd.com/");
            TapCommon.AddHost("https://moment.intl.tapapis.com/", "https://tds-moment-io.api.xdrnd.com/");
        }
        else
        {
            TapCommon.AddHost("https://openapi.tap.io", "https://open.api.xdrnd.com");
            TapCommon.AddHost("https://www.tap.io", "https://www.xdrnd.com");
            TapCommon.AddHost("https://tds-moment.tap.io/", "https://tds-moment-io.xdrnd.com/");
            TapCommon.AddHost("https://moment.intl.tapapis.com", "https://tds-moment-io.api.xdrnd.com");
        }

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
        InitTapSupport();
    }

    private void OnTapfriendClicked()
    {
    }

    private void OnCommonClicked()
    {
        SceneManager.LoadSceneAsync(4);
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

    public void TestTimer()
    {
        var webUrl = TapSupport.GetSupportWebUrl(TapSupportConstants.PathHome);
        Debug.Log($"webUrl:{webUrl}");


        var pathCateGory = TapSupport.GetSupportWebUrl(TapSupportConstants.PathCategory + "6108e403928aa97734554ef0",
            GETMetaData(), GETFieldsData());

        Debug.Log($"pathCateGory:{pathCateGory}");
    }

    private Dictionary<string, object> GETDefaultMetaMap()
    {
        Dictionary<string, object> testData = new Dictionary<string, object>();
        testData.Add("default_aaa", "default_a");
        testData.Add("default_bbb", 1111);
        testData.Add("default_ccc", true);
        return testData;
    }

    public Dictionary<string, object> GETDefaultFiledsMap()
    {
        Dictionary<string, object> testData = new Dictionary<string, object>();
        testData.Add("612c868565a05a00f081b11c", "default_a");
        testData.Add("6129df889b34d92ea85c59fa", 1111);
        testData.Add("6108ed29928aa97734557912", "服务器1");
        return testData;
    }


    public static Dictionary<string, object> GETMetaData()
    {
        Dictionary<string, object> testData = new Dictionary<string, object>();
        testData.Add("Meta_OS", "iOS 15.1");
        testData.Add("meta_test a", true);
        testData.Add("meta_test b", 1111111);
        testData.Add("ccccc", "abcd");
        return testData;
    }

    public static Dictionary<string, object> GETFieldsData()
    {
        Dictionary<string, object> testData = new Dictionary<string, object>();
        testData.Add("612c868565a05a00f081b11c", "xxxxx");
        testData.Add("6129df889b34d92ea85c59fa", 222);
        testData.Add("6108ed29928aa97734557912", "服务器1");
        return testData;
    }
}