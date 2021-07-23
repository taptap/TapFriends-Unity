using TapTap.Achievement;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementScene : MonoBehaviour, IAchievementCallback
{
    public Button fetAllAchievement;

    public Button fetUserAchievement;

    public Button localAllAchievement;

    public Button localUserAchievement;

    public InputField achievementId;

    public InputField achievementStep;

    public Toggle showToast;

    public Button showDialog;

    public Button back;

    public Text label;

    private string step;

    private string id;

    public Button makeAchievement;

    public Button growAchievement;

    public Button reachAchievement;

    // Start is called before the first frame update
    void Start()
    {
        TapAchievement.RegisterCallback(this);
        TapAchievement.InitData();

        makeAchievement.onClick.AddListener(() => { TapAchievement.MakeSteps(id, int.Parse(step)); });

        growAchievement.onClick.AddListener(() => { TapAchievement.GrowSteps(id, int.Parse(step)); });

        reachAchievement.onClick.AddListener(() => TapAchievement.Reach(id));

        fetAllAchievement.onClick.AddListener(() =>
        {
            TapAchievement.FetchAllAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label.text = $"获取所有成就失败:{code.code} message:{code.errorDescription}";
                }
                else if (list != null)
                {
                    label.text = "";
                    foreach (var bean in list)
                    {
                        label.text = label.text + bean.ToJson() + "\n";
                    }
                }
            });
        });


        fetUserAchievement.onClick.AddListener(() =>
        {
            TapAchievement.FetchUserAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label.text = $"获取用户成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label.text = "";
                    foreach (var bean in list)
                    {
                        label.text = label.text + bean.ToJson() + "\n";
                    }
                }
            });
        });

        localAllAchievement.onClick.AddListener(() =>
        {
            TapAchievement.GetLocalAllAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label.text = $"获取本地所有成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label.text = "";
                    foreach (var bean in list)
                    {
                        label.text = label.text + bean.ToJson() + "\n";
                    }
                }
            });
        });

        localUserAchievement.onClick.AddListener(() =>
        {
            TapAchievement.GetLocalUserAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label.text = $"获取本地用户成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label.text = "";
                    foreach (var bean in list)
                    {
                        label.text = label.text + bean.ToJson() + "\n";
                    }
                }
            });
        });

        achievementStep.onValueChanged.AddListener(s => { this.step = s; });
        achievementId.onValueChanged.AddListener(s => { this.id = s; });
        back.onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
        showDialog.onClick.AddListener(TapAchievement.ShowAchievementPage);
        showToast.onValueChanged.AddListener(TapAchievement.SetShowToast);
    }

    // Update is called once per frame

    public void OnAchievementSDKInitSuccess()
    {
        label.text = "成就初始化成功";
    }

    public void OnAchievementInitFail(TapError errorCode)
    {
        if (errorCode != null)
        {
            label.text = $"成就初始化失败:{errorCode.code}" + $"des:{errorCode.errorDescription}";
        }
    }

    public void OnAchievementStatusUpdate(TapAchievementBean bean, TapError errorCode)
    {
        if (errorCode != null)
        {
            label.text = $"成就状态更新失败Code:{errorCode.code}" + $"des:{errorCode.errorDescription}";
            return;
        }

        if (bean != null)
        {
            label.text = $"成就状态更新:{bean.ToJson()}";
        }
    }

    // private void OnGUI()
    // {
    //
    //     if (GUI.Button(new Rect(50, 50, 200, 60), "获取所有成就", myButtonStyle))
    //     {

    //     }
    //
    //     if (GUI.Button(new Rect(50, 150, 200, 60), "获取用户成就", myButtonStyle))
    //     {
    //         TapAchievement.FetchUserAchievementList((list, code) =>
    //         {
    //             if (code != null)
    //             {
    //                 label.text = $"获取用户成就失败:{code.code} Message:{code.errorDescription}";
    //             }
    //             else
    //             {
    //                 label.text = "";
    //                 foreach (var bean in list)
    //                 {
    //                     label.text = label.text + bean.ToJson() + "\n";
    //                 }
    //             }
    //         });
    //     }
    //
    //     if (GUI.Button(new Rect(50, 250, 200, 60), "返回", myButtonStyle))
    //     {
    //         SceneManager.LoadSceneAsync(0);
    //     }
    //
    //     reachId = GUI.TextArea(new Rect(300, 50, 200, 60), reachId);
    //
    //     step = GUI.TextArea(new Rect(300, 150, 200, 60), step);
    //
    //     if (GUI.Button(new Rect(300, 250, 200, 60), "本地所有成就", myButtonStyle))
    //     {
    //         TapAchievement.GetLocalAllAchievementList((list, code) =>
    //         {
    //             if (code != null)
    //             {
    //                 label.text = $"获取本地所有成就失败:{code.code} Message:{code.errorDescription}";
    //             }
    //             else
    //             {
    //                 label.text = "";
    //                 foreach (var bean in list)
    //                 {
    //                     label.text = label.text + bean.ToJson() + "\n";
    //                 }
    //             }
    //         });
    //     }
    //
    //     if (GUI.Button(new Rect(50, 350, 200, 60), "本地用户成就", myButtonStyle))
    //     {
    //         TapAchievement.GetLocalUserAchievementList((list, code) =>
    //         {
    //             if (code != null)
    //             {
    //                 label.text = $"获取本地所有成就失败:{code.code} Message:{code.errorDescription}";
    //             }
    //             else
    //             {
    //                 label.text = "";
    //                 foreach (var bean in list)
    //                 {
    //                     label.text = label.text + bean.ToJson() + "\n";
    //                 }
    //             }
    //         });
    //     }
    //
    //     if (GUI.Button(new Rect(50, 450, 200, 60), "取得指定成就", myButtonStyle))
    //     {
    //         TapAchievement.Reach(reachId);
    //     }
    //
    //     if (GUI.Button(new Rect(50, 550, 200, 60), "指定成就+1", myButtonStyle))
    //     {
    //         TapAchievement.GrowSteps(reachId, int.Parse(step));
    //     }
    //
    //     if (GUI.Button(new Rect(50, 650, 400, 60), "到达指定成就:" + reachId, myButtonStyle))
    //     {
    //         TapAchievement.MakeSteps(reachId, int.Parse(step));
    //     }
    //
    //     if (GUI.Button(new Rect(50, 750, 200, 60), "弹窗", myButtonStyle))
    //     {
    //         TapAchievement.ShowAchievementPage();
    //     }
    //
    //     if (GUI.Button(new Rect(300, 350, 200, 60), "显示Toast", myButtonStyle))
    //     {
    //         TapAchievement.SetShowToast(true);
    //     }
    //
    //     if (GUI.Button(new Rect(300, 450, 200, 60), "不显示Toast", myButtonStyle))
    //     {
    //         TapAchievement.SetShowToast(false);
    //     }
    // }
}