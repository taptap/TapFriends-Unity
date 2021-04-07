using UnityEngine;
using TapTap.Bootstrap;
using TapTap.TapDB;

public class TapDBScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TapBootstrap.GetUser((user, error) =>
        {
            if (user != null)
            {
                TapDB.Init("0RiAlMny7jiz086FaU", "channel", "gameVersion", true);
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
    private string userId = "请输入用户id";
    private string loginMethod = "请输入登录方式";
    private string staticEventKey = "请输入某个事件key";
    private string registerStaticEvent = "请输入json";

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 40;
        labelStyle.alignment = TextAnchor.MiddleLeft;
        labelStyle.normal.textColor = new Color(0, 0, 0);

        GUI.Label(new Rect(60, 150, 220, 70), "服务器", labelStyle);
        GUI.Label(new Rect(60, 260, 220, 70), "用户名", labelStyle);
        GUI.Label(new Rect(60, 370, 220, 70), "用户等级", labelStyle);
        GUI.Label(new Rect(60, 480, 80, 70), "充值", labelStyle);
        GUI.Label(new Rect(190, 480, 80, 70), "事件", labelStyle);
        //GUI.Label(new Rect(310, 480, 240, 70), "注册静态事件", labelStyle);
        //GUI.Label(new Rect(620, 480, 320, 70), "删除某个静态事件", labelStyle);
        GUI.Label(new Rect(60, 680, 240, 70), "注册动态事件", labelStyle);
        GUI.Label(new Rect(370, 680, 240, 70), "删除静态事件", labelStyle);
        GUI.Label(new Rect(680, 680, 280, 70), "初始化设备属性", labelStyle);
        GUI.Label(new Rect(60, 880, 240, 70), "更新设备属性", labelStyle);
        GUI.Label(new Rect(370, 880, 240, 70), "添加设备属性", labelStyle);
        GUI.Label(new Rect(680, 880, 350, 70), "初始化用户属性", labelStyle);
        GUI.Label(new Rect(60, 1080, 240, 70), "更新用户属性", labelStyle);
        GUI.Label(new Rect(370, 1080, 240, 70), "添加用户属性", labelStyle);
        GUI.Label(new Rect(60, 1280, 160, 70), "设置用户", labelStyle);


        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 35;
        serverName = GUI.TextArea(new Rect(310, 140, 350, 80), serverName, inputStyle);
        userName = GUI.TextArea(new Rect(310, 250, 350, 80), userName, inputStyle);
        level = GUI.TextArea(new Rect(310, 360, 350, 80), level, inputStyle);
        userId = GUI.TextArea(new Rect(310, 1290, 350, 80), userId, inputStyle);
        loginMethod = GUI.TextArea(new Rect(700, 1290, 350, 80), loginMethod, inputStyle);
        staticEventKey = GUI.TextArea(new Rect(620, 480, 360, 70), staticEventKey, inputStyle);
        registerStaticEvent = GUI.TextArea(new Rect(310, 480, 270, 70), registerStaticEvent, inputStyle);

        if (GUI.Button(new Rect(680, 130, 160, 80), "服务器", style))
        {
            TapDB.SetServer(serverName);
        }

        if (GUI.Button(new Rect(680, 240, 160, 80), "用户名", style))
        {
            TapDB.SetName(userName);
        }

        if (GUI.Button(new Rect(680, 350, 160, 80), "等级", style))
        {
            int rel = int.Parse(level);
            TapDB.SetLevel(rel);
        }

        if (GUI.Button(new Rect(60, 560, 90, 80), "充值", style))
        {
            TapDB.OnCharge("12345", "890", 1, "eur", "paypal");
        }

        if (GUI.Button(new Rect(190, 560, 90, 80), "事件", style))
        {
            TapDB.OnEvent("eventCode", "{\"event\":\"123\"}");
        }

        if (GUI.Button(new Rect(310, 560, 270, 80), "注册静态事件", style))
        {
            TapDB.RegisterStaticProperties(registerStaticEvent);
        }

        if (GUI.Button(new Rect(620, 560, 360, 80), "删除某个静态事件", style))
        {
            TapDB.UnregisterStaticProperty(staticEventKey);
        }

        if (GUI.Button(new Rect(370, 760, 270, 80), "删除静态事件", style))
        {
            TapDB.ClearStaticProperties();
        }

        if (GUI.Button(new Rect(680, 760, 310, 80), "初始化设备属性", style))
        {
            TapDB.DeviceInitialize("{\"device\":\"Initialize\"}");
        }

        if (GUI.Button(new Rect(60, 960, 270, 80), "更新设备属性", style))
        {
            TapDB.DeviceUpdate("{\"device\":\"update\"}");
        }

        if (GUI.Button(new Rect(370, 960, 270, 80), "添加设备属性", style))
        {
            TapDB.DeviceAdd("{\"device\":\"add\"}");
        }

        if (GUI.Button(new Rect(680, 960, 310, 80), "初始化用户属性", style))
        {
            TapDB.UserInitialize("{\"user\":\"Initialize\"}");
        }

        if (GUI.Button(new Rect(60, 1160, 270, 80), "更新用户属性", style))
        {
            TapDB.UserUpdate("{\"user\":\"update\"}");
        }

        if (GUI.Button(new Rect(370, 1160, 270, 80), "添加用户属性", style))
        {
            TapDB.UserAdd("{\"user\":\"add\"}");
        }

        if (GUI.Button(new Rect(60, 1390, 190, 80), "设置用户", style))
        {
            TapDB.SetUser(userId, loginMethod);
        }

        if (GUI.Button(new Rect(310, 1390, 190, 80), "跟踪事件", style))
        {
            TapDB.Track("eventName", "{\"trackEvent\":\"789\"}");
        }

        if (GUI.Button(new Rect(60, 1510, 160, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}