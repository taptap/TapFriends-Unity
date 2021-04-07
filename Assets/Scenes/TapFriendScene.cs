using UnityEngine;
using TapFriendsSDK;

public class TapFriendScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private string label = "";
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;
        
        var labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20
        };

        GUI.Label(new Rect(400, 400, 400, 300), label, labelStyle);
        
        if (GUI.Button(new Rect(60, 150, 280, 80), "添加好友", style))
        {
            TapFriends.AddFriend("110", error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 270, 280, 80), "删除好友", style))
        {
            TapFriends.DeleteFriend("110", error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
            });
        }

        if (GUI.Button(new Rect(60, 390, 280, 80), "获取关注列表", style))
        {
            TapFriends.GetFollowList(1,2,10, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
                else
                {
                    label = "获取关注列表成功";
                }
            });
        }

        if (GUI.Button(new Rect(60, 510, 280, 80), "获取粉丝列表", style))
        {
            TapFriends.GetFansList(1,10, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
                else
                {
                    label = "获取粉丝列表成功";
                }
            });
        }

        if (GUI.Button(new Rect(60, 630, 280, 80), "拉黑用户", style))
        {
            TapFriends.BlockUser("110", error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 750, 280, 80), "取消拉黑用户", style))
        {
            TapFriends.UnBlockUser("110", error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 870, 280, 80), "拉黑列表", style))
        {
            TapFriends.GetBlockList(1,10, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Descrption:{error.errorDescription}";
                }
                else
                {
                    label = "获取拉黑列表成功";
                }
            });
        }

        if (GUI.Button(new Rect(60, 990, 180, 80), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}
