using System.Collections;
using System.Collections.Generic;
using TapTap.Moment;
using UnityEngine;
using UnityEngine.UI;
using UnityNative.Toasts;

public class MomentScene : MonoBehaviour
{
    // Start is called before the first frame update

    //打开动态
    public Button openDynamic;

    //动态红点
    public Button dynamicRedPoint;

    //场景化入口
    public Button scenalizedEntry;

    //用户中心入口
    public Button userCenterEntry;

    //返回
    public Button back;

    //sceneId
    public InputField sceneidField;

    //userId
    public InputField useridField;

    //文本
    public Text info;

    void Start()
    {
        TapMoment.SetCallback((code, msg) =>
        {
            info.text = "---- moment 回调  code: " + code + " msg: " + msg + "----";
            Debug.Log("---- moment 回调  code: " + code + " msg: " + msg + "----");

            UnityNative.Toasts.Example.UnityNativeToastsHelper.ShowLongToast("code: " + code + " msg: " + msg);
        });

        openDynamic.onClick.AddListener(onOpenDynamicClicked);
        dynamicRedPoint.onClick.AddListener(onDynamicRedPointClicked);
        scenalizedEntry.onClick.AddListener(onScenalizedEntryClicked);
        userCenterEntry.onClick.AddListener(onUserCenterEntryClicked);
        back.onClick.AddListener(onBackClicked);
        sceneidField.onEndEdit.AddListener(onSceneidField);
        useridField.onEndEdit.AddListener(onUseridField);
    }

    private void onOpenDynamicClicked()
    {
        TapMoment.Open(Orientation.ORIENTATION_DEFAULT);
    }

    private void onDynamicRedPointClicked()
    {
        TapMoment.FetchNotification();
    }

    private void onScenalizedEntryClicked()
    {
        TapMoment.DirectlyOpen(Orientation.ORIENTATION_DEFAULT, TapMomentConstants.TapMomentPageShortCut,
            new Dictionary<string, object> {{TapMomentConstants.TapMomentPageShortCutKey, sceneId}});
    }

    private void onUserCenterEntryClicked()
    {
        TapMoment.DirectlyOpen(Orientation.ORIENTATION_DEFAULT, TapMomentConstants.TapMomentPageUser,
            new Dictionary<string, object> {{TapMomentConstants.TapMomentPageUserKey, userId}});
    }

    private void onBackClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    private void onSceneidField(string input)
    {
        Debug.Log($"SceneidField: {input} ==");
        sceneId = input;
    }

    private void onUseridField(string input)
    {
        Debug.Log($"UseridField: {input} ==");
        userId = input;
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        TapMoment.Close();
    }

    private string sceneId = "taprl0194610001";

    private string userId = "7KfeZgtnLAZvJG8JZUnYVw==";


    // private void OnGUI()
    // {
    // GUIStyle style = new GUIStyle(GUI.skin.button);
    // style.fontSize = 40;
    //
    // GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
    // inputStyle.fontSize = 35;
    //
    // sceneId = GUI.TextArea(new Rect(60, 450, 250, 100), sceneId, inputStyle);
    //
    // userId = GUI.TextArea(new Rect(60, 600, 250, 100), sceneId, inputStyle);
    //
    // var labelStyle = new GUIStyle(GUI.skin.label)
    // {
    //     fontSize = 20
    // };
    //
    // GUI.Label(new Rect(400, 100, 400, 300), label, labelStyle);
    //
    // GUI.depth = 0;

    // if (GUI.Button(new Rect(60, 150, 180, 100), "打开动态", style))
    // {
    //     TapMoment.Open(Orientation.ORIENTATION_DEFAULT);
    // }

    // if (GUI.Button(new Rect(60, 300, 180, 100), "动态红点", style))
    // {
    //     TapMoment.FetchNotification();
    // }

    // if (GUI.Button(new Rect(360, 450, 245, 100), "场景化入口", style))
    // {
    //     TapMoment.DirectlyOpen(Orientation.ORIENTATION_DEFAULT, TapMomentConstants.TapMomentPageShortCut,
    //         new Dictionary<string, object> {{TapMomentConstants.TapMomentPageShortCutKey, sceneId}});
    // }

    // if (GUI.Button(new Rect(360, 600, 245, 100), "用户中心入口", style))
    // {
    //     TapMoment.DirectlyOpen(Orientation.ORIENTATION_DEFAULT, TapMomentConstants.TapMomentPageUser,
    //         new Dictionary<string, object> {{TapMomentConstants.TapMomentPageUserKey, userId}});
    // }

    // if (GUI.Button(new Rect(60, 750, 180, 100), "返回", style))
    // {
    //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    // }
    // }

    // private void OnApplicationPause(bool focus)
    // {
    //     Debug.Log($"Moment Scene On Application:{focus}");
    //     //进入程序状态更改为前台
    //     if (focus)
    //     {
    //     }
    //     else
    //     {
    //         TapMoment.Close();
    //         //离开程序进入到后台状态
    //     }
    // }
}