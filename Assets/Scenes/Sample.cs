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
        
        if (GUI.Button(new Rect(60, 100, 280, 100), "国内", style))
        {
            TapBootstrap.Init(new TapConfig("0RiAlMny7jiz086FaU", true));
        }
        
        if (GUI.Button(new Rect(60, 300, 280, 100), "海外", style))
        {
            TapBootstrap.Init(new TapConfig("KFV9Pm9ojdmWkkRJeb", false));
        }

        if (GUI.Button(new Rect(60, 500, 280, 100), "登陆", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        if (GUI.Button(new Rect(60, 700, 280, 100), "动态", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }

        if (GUI.Button(new Rect(60, 900, 280, 100), "TapDB", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3);
        }
    }
}