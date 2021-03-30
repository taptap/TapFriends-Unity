using System.Collections;
using TapMoment;
using UnityEngine;

public class MomentScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TapMoment.TapMoment.Init("KFV9Pm9ojdmWkkRJeb", false);
        
        TapMoment.TapMoment.SetCallback((code, msg) =>
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

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        TapMoment.TapMoment.Close();
    }

    private string sceneId = "Please input sceneId";

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;

        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 35;

        sceneId = GUI.TextArea(new Rect(60, 450, 250, 100), sceneId, inputStyle);

        GUI.depth = 0;

        if (GUI.Button(new Rect(60, 150, 180, 100), "打开动态", style))
        {
            TapMoment.TapMoment.Open(Orientation.ORIENTATION_DEFAULT);
        }

        if (GUI.Button(new Rect(60, 300, 180, 100), "动态红点", style))
        {
            TapMoment.TapMoment.GetNoticeData();
        }

        if (GUI.Button(new Rect(360, 450, 245, 100), "场景化入口", style))
        {
            TapMoment.TapMoment.OpenSceneEntry(Orientation.ORIENTATION_DEFAULT, sceneId);
        }

        if (GUI.Button(new Rect(60, 600, 180, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}