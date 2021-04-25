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

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        if (GUI.Button(new Rect(60, 100, 280, 100), "RND", style))
        {
            var config = new TapConfig.TapConfigBuilder()
                .ClientID("uZ8Yy6cSXVOR6AMRPj")
                .ClientSecret("AVhR1Bu9qfLR1cGbZMAdZ5rzJSxfoEiQaFf1T2P7")
                .RegionType(RegionType.CN)
                .TapDBConfig("channel", "gameVersion")
                .Builder();

            TapBootstrap.Init(config);
        }

        if (GUI.Button(new Rect(60, 250, 280, 100), "海外", style))
        {
            TapBootstrap.Init(new TapConfig("KFV9Pm9ojdmWkkRJeb", "7mpVJdXIOLQxvQdqjEEpiz7eLf82cMwYkdgoAZqF",
                RegionType.IO));
        }

        if (GUI.Button(new Rect(60, 400, 280, 100), "国内", style))
        {
            TapBootstrap.Init(new TapConfig("0RiAlMny7jiz086FaU", "8V8wemqkpkxmAN7qKhvlh6v0pXc8JJzEZe3JFUnU",
                RegionType.CN));
        }

        if (GUI.Button(new Rect(60, 550, 280, 100), "登陆", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        if (GUI.Button(new Rect(60, 700, 280, 100), "动态", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }

        if (GUI.Button(new Rect(60, 850, 280, 100), "TapDB", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        }

        if (GUI.Button(new Rect(60, 1000, 280, 100), "TapFriend", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4);
        }
    }
}