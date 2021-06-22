﻿using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TapTap.Bootstrap;
using TapTap.TapDB;
using TapTap.Common;
using JudgeDevice;

public class TapDBScene : MonoBehaviour, IDynamicProperties
{
    // Start is called before the first frame update
    void Start()
    {
        Judge.JudgeDeviceModel();
        TapBootstrap.GetUser((user, error) =>
        {
            if (user != null)
            {
                TapDB.EnableLog(true);
                TapDB.SetUser(user.name);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string serverName = "serverName";
    private string userName = "userName";
    private string level = "1";
    private string events = "事件json";
    private string registerStaticEvent = "静态事件json";
    private string deleteStaticEvent = "请输入某个事件key";
    private string deviceInitialize = "设备初始化json";
    private string deviceUpdate = "更新设备json";
    private string deviceAdd = "添加设备json";
    private string userInitialize = "用户初始化json";
    private string userUpdate = "更新用户json";
    private string userAdd = "添加用户json";
    private string userId = "请输入用户id";
    private string loginMethod = "请输入登录方式";
    private string trackEvent = "跟踪事件json";
    private string registerDynamicEvent = "动态事件json";


    public Dictionary<string, object> GetDynamicProperties()
    {
        Dictionary<string, object> dic = Json.Deserialize(registerDynamicEvent) as Dictionary<string, object>;
        return dic;
        // var dic = new Dictionary<string, object> {{"time", DateTime.Now.ToString(CultureInfo.InvariantCulture)}};
        // return dic;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = Judge.IsIphoneXDevice ? 40 : 30;

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = Judge.IsIphoneXDevice ? 40 : 30;
        labelStyle.alignment = TextAnchor.MiddleLeft;
        labelStyle.normal.textColor = new Color(0, 0, 0);

        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = Judge.IsIphoneXDevice ? 35 : 25;
        //服务器
        serverName = GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 100 : 0, 320, Judge.IsIphoneXDevice ? 70 : 50),
            serverName, inputStyle);
        //用户名
        userName = GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 270 : 130, 320, Judge.IsIphoneXDevice ? 70 : 50),
            userName, inputStyle);
        //等级
        level = GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 440 : 260, 320, Judge.IsIphoneXDevice ? 70 : 50),
            level, inputStyle);
        //事件
        events = GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 610 : 390, 320, Judge.IsIphoneXDevice ? 70 : 50),
            events, inputStyle);
        //初始化设备属性
        deviceInitialize =
            GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 780 : 520, 320, Judge.IsIphoneXDevice ? 70 : 50),
                deviceInitialize, inputStyle);
        //更新设备属性
        deviceUpdate =
            GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 950 : 650, 320, Judge.IsIphoneXDevice ? 70 : 50),
                deviceUpdate, inputStyle);
        //添加设备属性
        deviceAdd = GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 1120 : 780, 320, Judge.IsIphoneXDevice ? 70 : 50),
            deviceAdd, inputStyle);
        //注册静态事件
        registerStaticEvent =
            GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 1290 : 910, 320, Judge.IsIphoneXDevice ? 70 : 50),
                registerStaticEvent, inputStyle);
        //删除某个静态事件
        deleteStaticEvent =
            GUI.TextArea(new Rect(60, Judge.IsIphoneXDevice ? 1460 : 1040, 320, Judge.IsIphoneXDevice ? 70 : 50),
                deleteStaticEvent, inputStyle);
//-----------------------------------------------------------------------------------
        //初始化用户属性
        userInitialize =
            GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 100 : 0, 320, Judge.IsIphoneXDevice ? 70 : 50),
                userInitialize, inputStyle);
        //更新用户属性
        userUpdate =
            GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 270 : 130, 320, Judge.IsIphoneXDevice ? 70 : 50),
                userUpdate, inputStyle);
        //添加用户属性
        userAdd = GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 440 : 260, 320, Judge.IsIphoneXDevice ? 70 : 50),
            userAdd, inputStyle);
        //跟踪事件
        trackEvent =
            GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 610 : 390, 320, Judge.IsIphoneXDevice ? 70 : 50),
                trackEvent, inputStyle);
        //注册动态事件
        registerDynamicEvent =
            GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 780 : 520, 320, Judge.IsIphoneXDevice ? 70 : 50),
                registerDynamicEvent, inputStyle);
        //输入用户ID
        userId = GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 950 : 650, 320, Judge.IsIphoneXDevice ? 70 : 50),
            userId, inputStyle);
        //输入登录方式
        loginMethod =
            GUI.TextArea(new Rect(420, Judge.IsIphoneXDevice ? 1020 : 700, 320, Judge.IsIphoneXDevice ? 70 : 50),
                loginMethod, inputStyle);


        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 170 : 50, 160, Judge.IsIphoneXDevice ? 70 : 50), "服务器",
            style))
        {
            TapDB.SetServer(serverName);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 340 : 180, 160, Judge.IsIphoneXDevice ? 70 : 50), "用户名",
            style))
        {
            TapDB.SetName(userName);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 510 : 310, 160, Judge.IsIphoneXDevice ? 70 : 50), "等级",
            style))
        {
            int rel = int.Parse(level);
            TapDB.SetLevel(rel);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 680 : 440, 120, Judge.IsIphoneXDevice ? 70 : 50), "事件",
            style))
        {
            //"{"events":"Initialize"}"
            TapDB.OnEvent("eventCode", events);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 850 : 570, 310, Judge.IsIphoneXDevice ? 70 : 50), "初始化设备属性",
            style))
        {
            //"{\"device\":\"Initialize\"}"
            TapDB.DeviceInitialize(deviceInitialize);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1020 : 700, 270, Judge.IsIphoneXDevice ? 70 : 50), "更新设备属性",
            style))
        {
            //"{\"device\":\"update\"}"
            TapDB.DeviceUpdate(deviceUpdate);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1190 : 830, 270, Judge.IsIphoneXDevice ? 70 : 50), "添加设备属性",
            style))
        {
            //"{\"device\":\"add\"}"
            TapDB.DeviceAdd(deviceAdd);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1360 : 960, 270, Judge.IsIphoneXDevice ? 70 : 50), "注册静态事件",
            style))
        {
            //"{"RegisterStaticProperties":"static"}"
            TapDB.RegisterStaticProperties(registerStaticEvent);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice ? 1530 : 1090, 360, Judge.IsIphoneXDevice ? 70 : 50),
            "删除某个静态事件", style))
        {
            TapDB.UnregisterStaticProperty(deleteStaticEvent);
        }

//-----------------------------------------------------------------------------------
        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 170 : 50, 310, Judge.IsIphoneXDevice ? 70 : 50), "初始化用户属性",
            style))
        {
            //"{\"user\":\"Initialize\"}"
            TapDB.UserInitialize(userInitialize);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 340 : 180, 270, Judge.IsIphoneXDevice ? 70 : 50), "更新用户属性",
            style))
        {
            //"{\"user\":\"update\"}"
            TapDB.UserUpdate(userUpdate);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 510 : 310, 270, Judge.IsIphoneXDevice ? 70 : 50), "添加用户属性",
            style))
        {
            //"{\"user\":\"add\"}"
            TapDB.UserAdd(userAdd);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 680 : 440, 190, Judge.IsIphoneXDevice ? 70 : 50), "跟踪事件",
            style))
        {
            //"{\"trackEvent\":\"789\"}"
            TapDB.TrackEvent("eventName", trackEvent);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 850 : 570, 270, Judge.IsIphoneXDevice ? 70 : 50), "注册动态事件",
            style))
        {
            //"{"hahah":"xxxxxxxxx"}"
            TapDBImpl.GetInstance().RegisterDynamicProperties(this);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 1090 : 750, 190, Judge.IsIphoneXDevice ? 70 : 50), "设置用户",
            style))
        {
            TapDB.SetUser(userId, loginMethod);
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 1190 : 830, 90, Judge.IsIphoneXDevice ? 70 : 50), "充值",
            style))
        {
            TapDB.OnCharge("12345", "890", 1, "eur", "paypal");
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 1290 : 910, 270, Judge.IsIphoneXDevice ? 70 : 50),
            "删除静态事件", style))
        {
            TapDB.ClearStaticProperties();
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 1390 : 990, 100, Judge.IsIphoneXDevice ? 70 : 50),
            "取消DeviceID", style))
        {
            TapDB.CloseFetchTapTapDeviceId();
        }

        if (GUI.Button(new Rect(420, Judge.IsIphoneXDevice ? 1490 : 1070, 100, Judge.IsIphoneXDevice ? 70 : 50), "返回",
            style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}