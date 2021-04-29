using TapTap.Bootstrap;
using UnityEngine;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private bool isSwitch = true;
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;
        
        // GUIStyle myToggleStyle = new GUIStyle(GUI.skin.toggle)
        // {
        //     fontSize = 20
        // };
        // GUI.Toggle(new Rect(380, 100, 120, 30), isSwitch, "TapDB开关",myToggleStyle);

        if (GUI.Button(new Rect(60, 100, 280, 100), "RND-IO", style))
        {
            var config = new TapConfig.TapConfigBuilder()
                .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientSecret("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .RegionType(RegionType.IO)
                .TapDBConfig(true, "channel", "gameVersion")
                .Builder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, 250, 280, 100), "RND-CN", style))
        {
            var config = new TapConfig.TapConfigBuilder()
                .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientSecret("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .RegionType(RegionType.CN)
                .TapDBConfig(true, "channel", "gameVersion")
                .Builder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, 400, 280, 100), "海外", style))
        {
            var config = new TapConfig.TapConfigBuilder()
                .ClientID("KFV9Pm9ojdmWkkRJeb")
                .ClientSecret("7mpVJdXIOLQxvQdqjEEpiz7eLf82cMwYkdgoAZqF")
                .RegionType(RegionType.IO)
                .EnableTapDB(false)
                .Builder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, 550, 280, 100), "国内", style))
        {
            var config = new TapConfig.TapConfigBuilder()
                .ClientID("0RiAlMny7jiz086FaU")
                .ClientSecret("8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU")
                .RegionType(RegionType.CN)
                .TapDBConfig(true, "channel", "gameVersion")
                .Builder();
            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, 700, 280, 100), "登陆", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        if (GUI.Button(new Rect(60, 850, 280, 100), "动态", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }

        if (GUI.Button(new Rect(60, 1000, 280, 100), "TapDB", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        }

        if (GUI.Button(new Rect(60, 1150, 280, 100), "TapFriend", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
        }
    }
}