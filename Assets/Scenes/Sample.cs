using System.Collections;
using TapTap.Bootstrap;
using UnityEngine;
using UnityEngine.UI;
using TapTap.License;
using TapTap.TapDB;
using JudgeDevice;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update

    private Toggle switchButton;

    void Start()
    {
        Judge.JudgeDeviceModel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private bool isSwitch = true;
    private bool isIDFA = true;
    private string channel = "输入TapDB：channel";
    private string gameVersion = "输入TapDB：gameVersion";

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

        isSwitch = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice?100:0, 200, 55), isSwitch, "TapDB开关", myToggleStyle);

        isIDFA = GUI.Toggle(new Rect(380, Judge.IsIphoneXDevice?200:60, 200, 55), isIDFA, "IDFA 开关", myToggleStyle);

        channel = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice?280:135, 330, 70), channel, inputStyle);

        gameVersion = GUI.TextArea(new Rect(380, Judge.IsIphoneXDevice?360:215, 330, 70), gameVersion, inputStyle);

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?100:0, 280, 100), "RND-IO", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientSecret("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .RegionType(RegionType.IO)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
                .ConfigBuilder();
            
            TapDB.AdvertiserIDCollectionEnabled(isIDFA);
            
            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?250:150, 280, 100), "RND-CN", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientSecret("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .RegionType(RegionType.CN)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(), isIDFA)
                .ConfigBuilder();

            TapDB.AdvertiserIDCollectionEnabled(isIDFA);
            
            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?400:300, 280, 100), "海外", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("KFV9Pm9ojdmWkkRJeb")
                .ClientSecret("7mpVJdXIOLQxvQdqjEEpiz7eLf82cMwYkdgoAZqF")
                .RegionType(RegionType.IO)
                .EnableTapDB(isSwitch)
                .ConfigBuilder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?550:450, 280, 100), "国内", style))
        {
            var config = new TapConfig.Builder()
                .ClientID("0RiAlMny7jiz086FaU")
                .ClientSecret("8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU")
                .RegionType(RegionType.CN)
                .TapDBConfig(isSwitch, channelValue(), gameVersionValue(),isIDFA)
                .ConfigBuilder();
            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?700:600, 280, 100), "登陆", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?850:750, 280, 100), "动态", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }

        if (GUI.Button(new Rect(60, Judge.IsIphoneXDevice?1000:900, 280, 100), "TapDB", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        }

        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice?470:325, 280, 100), "TapFriend", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
        }

        if (GUI.Button(new Rect(380, Judge.IsIphoneXDevice?610:465, 280, 100), "Common", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(5);
        }
    }
}