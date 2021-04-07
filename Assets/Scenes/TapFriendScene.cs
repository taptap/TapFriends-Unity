using UnityEngine;
using TapTap.Friends;

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
    
    private string from = "0";
    private string limit = "5";
    private string userId = "f05baed5b5f04eeead7c489267309c1c";
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 40;
        
        var labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 25
        };
        GUI.Label(new Rect(400, 400, 400, 300), label, labelStyle);
        
        GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        inputStyle.fontSize = 35;
        from = GUI.TextArea(new Rect(60, 140, 200, 80), from, inputStyle);
        limit = GUI.TextArea(new Rect(290, 140, 200, 80), limit, inputStyle);
        userId = GUI.TextArea(new Rect(520, 140, 380, 80), userId, inputStyle);
        
        if (GUI.Button(new Rect(60, 260, 280, 80), "添加好友", style))
        {
            TapFriends.AddFriend(userId, error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    label = "添加好友成功";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 380, 280, 80), "删除好友", style))
        {
            TapFriends.DeleteFriend(userId, error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    label = "删除好友成功";
                }
            });
        }

        if (GUI.Button(new Rect(60, 500, 280, 80), "好友列表", style))
        {
            int handleFrom = int.Parse(from);
            int handleLimit = int.Parse(limit);
            TapFriends.GetFollowingList(handleFrom,0,handleLimit, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    if (list.Count != 0)
                    {
                        label = "获取好友列表成功: ";
                        foreach (TapUserRelationShip relation in list)
                        {
                            this.label = this.label + relation.ToJson();
                        } 
                    }
                    else
                    {
                        this.label = "获取好友列表为空"; 
                    }
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 620, 300, 80), "互关好友列表", style))
        {
            int handleFrom = int.Parse(from);
            int handleLimit = int.Parse(limit);
            TapFriends.GetFollowingList(handleFrom,1,handleLimit, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    if (list.Count != 0)
                    {
                        this.label = "获取互关好友列表成功: ";
                        foreach (TapUserRelationShip relation in list)
                        {
                            this.label = this.label + relation.ToJson();
                        } 
                    }
                    else
                    {
                        this.label = "获取互关好友列表为空";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(60, 740, 280, 80), "获取粉丝列表", style))
        {
            int handleFrom = int.Parse(from);
            int handleLimit = int.Parse(limit);
            TapFriends.GetFollowerList(handleFrom,handleLimit, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    if (list.Count != 0)
                    {
                        this.label = "获取粉丝列表成功: ";
                        foreach (TapUserRelationShip relation in list)
                        {
                            this.label = this.label + relation.ToJson();
                        } 
                    }
                    else
                    {
                        this.label = "获取粉丝列表为空"; 
                    }
                }
            });
        }

        if (GUI.Button(new Rect(60, 860, 280, 80), "拉黑用户", style))
        {
            TapFriends.BlockUser(userId, error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    label = "拉黑用户成功";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 980, 280, 80), "取消拉黑用户", style))
        {
            TapFriends.UnblockUser(userId, error =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    label = "取消拉黑用户成功";
                }
            });
        }
        
        if (GUI.Button(new Rect(60, 1100, 280, 80), "拉黑列表", style))
        {
            int handleFrom = int.Parse(from);
            int handleLimit = int.Parse(limit);
            TapFriends.GetBlockList(handleFrom,handleLimit, (list, error) =>
            {
                if (error != null)
                {
                    label = $"Error:{error.code} Description:{error.errorDescription}";
                }
                else
                {
                    if (list.Count != 0)
                    {
                        label = "获取拉黑列表成功";
                        foreach (TapUserRelationShip relation in list)
                        {
                            this.label = this.label + relation.userId +
                                         relation.name +
                                         relation.avatar +
                                         relation.gender +
                                         relation.mutualAttention;
                        }  
                    }
                    else
                    {
                        label = "获取拉黑列表为空";
                    }
                }
            });
        }

        if (GUI.Button(new Rect(60, 1220, 180, 80), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}
