using TapTap.Achievement;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementScene : MonoBehaviour, IAchievementCallback
{
    // Start is called before the first frame update
    void Start()
    {
        TapAchievement.RegisterCallback(this);
        TapAchievement.InitData();
    }

    // Update is called once per frame
    void Update()
    {
    }

    string reachId = "C13";

    string step = "10";

    private string label = "";

    public void OnAchievementSDKInitSuccess()
    {
        label = "成就初始化成功";
    }

    public void OnAchievementInitFail(TapError errorCode)
    {
        if (errorCode != null)
        {
            label = $"成就初始化失败:{errorCode.code}" + $"des:{errorCode.errorDescription}";
        }
    }

    public void OnAchievementStatusUpdate(TapAchievementBean bean, TapError errorCode)
    {
        if (errorCode != null)
        {
            label = $"成就状态更新失败Code:{errorCode.code}" + $"des:{errorCode.errorDescription}";
            return;
        }

        if (bean != null)
        {
            label = $"成就状态更新:{bean.ToJson()}";
        }
    }

    private void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 30
        };

        GUI.depth = 0;

        var labelStyle = new GUIStyle(GUI.skin.label) {fontSize = 25, normal = {textColor = new Color(255, 0, 0)}};

        GUI.Label(
            new Rect(550, 50, 500, 1300),
            label, labelStyle);

        if (GUI.Button(new Rect(50, 50, 200, 60), "获取所有成就", myButtonStyle))
        {
            TapAchievement.FetchAllAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label = $"获取所有成就失败:{code.code} message:{code.errorDescription}";
                }
                else if (list != null)
                {
                    label = "";
                    foreach (var bean in list)
                    {
                        label = label + bean.ToJson() + "\n";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(50, 150, 200, 60), "获取用户成就", myButtonStyle))
        {
            TapAchievement.FetchUserAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label = $"获取用户成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label = "";
                    foreach (var bean in list)
                    {
                        label = label + bean.ToJson() + "\n";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(50, 250, 200, 60), "返回", myButtonStyle))
        {
            SceneManager.LoadSceneAsync(0);
        }

        reachId = GUI.TextArea(new Rect(300, 50, 200, 60), reachId);

        step = GUI.TextArea(new Rect(300, 150, 200, 60), step);

        if (GUI.Button(new Rect(300, 250, 200, 60), "本地所有成就", myButtonStyle))
        {
            TapAchievement.GetLocalAllAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label = $"获取本地所有成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label = "";
                    foreach (var bean in list)
                    {
                        label = label + bean.ToJson() + "\n";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(50, 350, 200, 60), "本地用户成就", myButtonStyle))
        {
            TapAchievement.GetLocalUserAchievementList((list, code) =>
            {
                if (code != null)
                {
                    label = $"获取本地所有成就失败:{code.code} Message:{code.errorDescription}";
                }
                else
                {
                    label = "";
                    foreach (var bean in list)
                    {
                        label = label + bean.ToJson() + "\n";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(50, 450, 200, 60), "取得指定成就", myButtonStyle))
        {
            TapAchievement.Reach(reachId);
        }

        if (GUI.Button(new Rect(50, 550, 200, 60), "指定成就+1", myButtonStyle))
        {
            TapAchievement.GrowSteps(reachId, int.Parse(step));
        }

        if (GUI.Button(new Rect(50, 650, 400, 60), "到达指定成就:" + reachId, myButtonStyle))
        {
            TapAchievement.MakeSteps(reachId, int.Parse(step));
        }

        if (GUI.Button(new Rect(50, 750, 200, 60), "弹窗", myButtonStyle))
        {
            TapAchievement.ShowAchievementPage();
        }

        if (GUI.Button(new Rect(300, 350, 200, 60), "显示Toast", myButtonStyle))
        {
            TapAchievement.SetShowToast(true);
        }

        if (GUI.Button(new Rect(300, 450, 200, 60), "不显示Toast", myButtonStyle))
        {
            TapAchievement.SetShowToast(false);
        }
    }
}
