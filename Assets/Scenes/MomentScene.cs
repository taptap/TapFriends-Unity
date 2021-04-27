using System.Collections;
using TapTap.Moment;
using UnityEngine;

public class MomentScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TapMoment.SetCallback((code, msg) =>
        {
            Debug.Log("---- moment 回调  code: " + code + " msg: " + msg + "----");
            if (code == 20100)
            {
                
            }
            else if (code == 20000)
            {
                
            }
        });
    }
    
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        TapMoment.Close();
    }

    private string sceneId = "Input sceneId";

    private string userId = "Input UserId";

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 35;

        sceneId = GUI.TextArea(new Rect(60, 450, 250, 100), sceneId, inputStyle);

        userId = GUI.TextArea(new Rect(60, 600, 250, 100), sceneId, inputStyle);

        GUI.depth = 0;

        if (GUI.Button(new Rect(60, 150, 180, 100), "打开动态", style))
        {
            TapMoment.Open(Orientation.ORIENTATION_DEFAULT);
        }

        if (GUI.Button(new Rect(60, 300, 180, 100), "动态红点", style))
        {
            TapMoment.FetchNotification();
        }

        if (GUI.Button(new Rect(360, 450, 245, 100), "场景化入口", style))
        {
        }

        if (GUI.Button(new Rect(360, 600, 245, 100), "用户中心入口", style))
        {
        }

        if (GUI.Button(new Rect(60, 750, 180, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}