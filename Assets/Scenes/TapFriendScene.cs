using UnityEngine;
using TapTap.Friends;
using System.Collections.Generic;
using System.Linq;
using TapTap.Common;
using JudgeDevice;
using UnityEngine.UI;

public class TapFriendScene : MonoBehaviour, ITapMessageListener
{
    // Start is called before the first frame update
    //添加好友
    public Button addFriend;
    //搜索好友
    public Button searchFriend;
    //删除好友
    public Button deleteFriend;
    //好友邀请链接
    public Button inviteFriendLink;
    //好友列表
    public Button friendList;
    //分享好友邀请
    public Button shareFriendInvite;
    //互关好友列表
    public Button mutualFriendList;
    //取消拉黑用户
    public Button cancelBlockUser;
    //获取粉丝列表
    public Button fansList;
    //拉黑列表
    public Button blockList;
    //拉黑用户
    public Button blockUser;
    //设置富信息
    public Button settingRichInfo;
    //取消富信息
    public Button cancelRichInfo;
    //返回
    public Button back;
    //fronField
    public InputField fromField;
    //limitField
    public InputField limitField;
    //useridField
    public InputField userIdField;
    //richKeyField
    public InputField richKeyField;
    //richValueField
    public InputField richValueField;
    //文本
    public Text label;
    
    void Start()
    {
        TapFriends.RegisterMessageListener(this);
        
        addFriend.onClick.AddListener(OnAddFriendClicked);
        searchFriend.onClick.AddListener(OnSearchFriendClicked);
        deleteFriend.onClick.AddListener(OnDeleteFriendClicked);
        inviteFriendLink.onClick.AddListener(OnInviteFriendLinkClicked);
        friendList.onClick.AddListener(OnFriendListClicked);
        shareFriendInvite.onClick.AddListener(OnShareFriendInviteClicked);
        mutualFriendList.onClick.AddListener(OnMutualFriendListClicked);
        cancelBlockUser.onClick.AddListener(OnCancelBlockUserClicked);
        fansList.onClick.AddListener(OnFansListClicked);
        blockList.onClick.AddListener(OnBlockListClicked);
        blockUser.onClick.AddListener(OnBlockUserClicked);
        settingRichInfo.onClick.AddListener(OnSettingRichInfoClicked);
        cancelRichInfo.onClick.AddListener(OnCancelRichInfoClicked);
        back.onClick.AddListener(OnBackClicked);

        fromField.onEndEdit.AddListener(OnFromField);
        limitField.onEndEdit.AddListener(OnLimitField);
        userIdField.onEndEdit.AddListener(OnUserIdField);
        richKeyField.onEndEdit.AddListener(OnRichKeyField);
        richValueField.onEndEdit.AddListener(OnRichValueField);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnAddFriendClicked()
    {
        TapFriends.AddFriend(userId, error =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "添加好友成功";
            }
        });
    }
    private void OnSearchFriendClicked()
    {
        TapFriends.SearchUser(userId, (relation, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                var str = "搜索用户成功";
                var richPresence = relation.richPresence.Aggregate("",
                    (current, richEntry) =>
                        current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));

                str = str + "userId：" + relation.userId +
                      " name：" + relation.name +
                      " avatar：" + relation.avatar +
                      " gender：" + relation.gender +
                      " mutualAttention：" + relation.mutualAttention +
                      " relationship：" + relation.relationship +
                      " onLine：" + relation.online +
                      " time：" + relation.time +
                      " rich_presence:" + richPresence
                    ;
                
                label.text = str;
            }
        });
    }
    private void OnDeleteFriendClicked()
    {
        TapFriends.DeleteFriend(userId, error =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "删除好友成功";
            }
        });
    }
    private void OnInviteFriendLinkClicked()
    {
        TapFriends.GenerateFriendInvitation((invitationString, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "获取好友邀请链接成功: " + invitationString;
            }
        });
    }
    private void OnFriendListClicked()
    {
        int handleFrom = int.Parse(from);
        int handleLimit = int.Parse(limit);
        TapFriends.GetFollowingList(handleFrom, false, handleLimit, (list, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                if (list.Count != 0)
                {
                    label.text = "获取好友列表成功: ";
                    foreach (TapUserRelationShip relation in list)
                    {
                        var richPresence = relation.richPresence.Aggregate("",
                            (current, richEntry) =>
                                current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));

                        label.text = label.text + "userId：" + relation.userId +
                                     " name：" + relation.name +
                                     " avatar：" + relation.avatar +
                                     " gender：" + relation.gender +
                                     " mutualAttention：" + relation.mutualAttention +
                                     " onLine：" + relation.online +
                                     " time：" + relation.time +
                                     " relationship：" + relation.relationship +
                                     " rich_presence:" + richPresence
                            ;
                    }
                }
                else
                {
                    label.text = "获取好友列表为空";
                }
            }
        });
    }
    private void OnShareFriendInviteClicked()
    {
        TapFriends.SendFriendInvitation((isInvitation, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "分享好友邀请: " + (isInvitation ? "成功" : "失败");
            }
        });
    }
    private void OnMutualFriendListClicked()
    {
        int handleFrom = int.Parse(from);
        int handleLimit = int.Parse(limit);
        TapFriends.GetFollowingList(handleFrom, true, handleLimit, (list, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                if (list.Count != 0)
                {
                    this.label.text = "获取互关好友列表成功: ";
                    foreach (TapUserRelationShip relation in list)
                    {
                        var richPresence = relation.richPresence.Aggregate("",
                            (current, richEntry) =>
                                current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));

                        label.text = label.text + "userId：" + relation.userId +
                                     " name：" + relation.name +
                                     " avatar：" + relation.avatar +
                                     " gender：" + relation.gender +
                                     " onLine：" + relation.online +
                                     " time：" + relation.time +
                                     " mutualAttention：" + relation.mutualAttention +
                                     " relationship：" + relation.relationship +
                                     " rich_presence:" + richPresence
                            ;
                    }
                }
                else
                {
                    label.text = "获取互关好友列表为空";
                }
            }
        });
    }
    private void OnCancelBlockUserClicked()
    {
        TapFriends.UnblockUser(userId, error =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "取消拉黑用户成功";
            }
        });
    }
    private void OnFansListClicked()
    {
        int handleFrom = int.Parse(from);
        int handleLimit = int.Parse(limit);
        TapFriends.GetFollowerList(handleFrom, handleLimit, (list, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                if (list.Count != 0)
                {
                    label.text = "获取粉丝列表成功: ";
                    foreach (TapUserRelationShip relation in list)
                    {
                        var richPresence = relation.richPresence.Aggregate("",
                            (current, richEntry) =>
                                current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));

                        label.text = label.text + "userId：" + relation.userId +
                                     " name：" + relation.name +
                                     " avatar：" + relation.avatar +
                                     " gender：" + relation.gender +
                                     " onLine：" + relation.online +
                                     " time：" + relation.time +
                                     " mutualAttention：" + relation.mutualAttention +
                                     " relationship：" + relation.relationship +
                                     " rich_presence:" + richPresence
                            ;
                    }
                }
                else
                {
                    label.text = "获取粉丝列表为空";
                }
            }
        });
    }
    private void OnBlockListClicked()
    {
        int handleFrom = int.Parse(from);
        int handleLimit = int.Parse(limit);
        TapFriends.GetBlockList(handleFrom, handleLimit, (list, error) =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                if (list.Count != 0)
                {
                    label.text = "获取拉黑列表成功";
                    foreach (TapUserRelationShip relation in list)
                    {
                        var richPresence = relation.richPresence.Aggregate("",
                            (current, richEntry) =>
                                current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));

                        label.text = label.text + "userId：" + relation.userId +
                                     " name：" + relation.name +
                                     " avatar：" + relation.avatar +
                                     " gender：" + relation.gender +
                                     " onLine：" + relation.online +
                                     " time：" + relation.time +
                                     " mutualAttention：" + relation.mutualAttention +
                                     " relationship：" + relation.relationship +
                                     " rich_presence:" + richPresence
                            ;
                    }
                }
                else
                {
                    label.text = "获取拉黑列表为空";
                }
            }
        });
    }
    private void OnBlockUserClicked()
    {
        TapFriends.BlockUser(userId, error =>
        {
            if (error != null)
            {
                label.text = $"Error:{error.code} Description:{error.errorDescription}";
            }
            else
            {
                label.text = "拉黑用户成功";
            }
        });
    }
    private void OnSettingRichInfoClicked()
    {
        TapFriends.SetRichPresence(richKey, richValue, error =>
        {
            if (error != null)
            {
                label.text = $"设置富信息失败:{error.errorDescription}";
            }
            else
            {
                label.text = "设置富信息成功";
            }
        });
    }
    private void OnCancelRichInfoClicked()
    {
        TapFriends.ClearRichPresence(richKey, error =>
        {
            if (error != null)
            {
                label.text = $"取消富信息失败:{error.errorDescription}";
            }
            else
            {
                label.text = "取消富信息成功";
            }
        });
    }
    private void OnBackClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    private void OnFromField(string input)
    {
        from = input;
        Debug.Log($"FromField: {input} ==");
    }
    
    private void OnLimitField(string input)
    {
        limit = input;
        Debug.Log($"LimitField: {input} ==");
    }
    
    private void OnUserIdField(string input)
    {
        userId = input;
        Debug.Log($"UserIdField: {input} ==");
    }
    
    private void OnRichKeyField(string input)
    {
        richKey = input;
        Debug.Log($"RichKeyField: {input} ==");
    }
    
    private void OnRichValueField(string input)
    {
        richValue = input;
        Debug.Log($"RichValueField: {input} ==");
    }

    public void OnMessageWithCode(int code, Dictionary<string, object> extras)
    {
        label.text = "接收消息：" + "code: " + code + " extras: " + Json.Serialize(extras);
    }

    // private string label = "";
    private string from = "0";
    private string limit = "5";
    private string userId = "d669eda7fb704e08b1734a590ed899cc";

    private string richKey = "key";

    private string richValue = "value";

    // private void OnGUI()
    // {
        // float lowHeiht = 50;
        // float fx = Judge.IsIphoneXDevice ? 60 : 30;
        // float sx = Judge.IsIphoneXDevice ? 380 : 340;
        // float height = Judge.IsIphoneXDevice ? 80 : lowHeiht;
        // float margin = 30;
        // float space = lowHeiht + margin;
        //
        // GUIStyle style = new GUIStyle(GUI.skin.button);
        // style.fontSize = Judge.IsIphoneXDevice ? 40 : 30;
        //
        // var labelStyle = new GUIStyle(GUI.skin.label);
        // labelStyle.fontSize = 25;
        // labelStyle.normal.textColor = new Color(255, 0, 0);
        //
        // GUI.Label(
        //     new Rect(Judge.IsIphoneXDevice ? 60 : 30, Judge.IsIphoneXDevice ? 1150 : 9 * space + lowHeiht, 500, 1300),
        //     label, labelStyle);

        // GUIStyle inputStyle = new GUIStyle(GUI.skin.textArea);
        // inputStyle.fontSize = 35;

        // from = GUI.TextArea(new Rect(fx, Judge.IsIphoneXDevice ? 140 : 0, Judge.IsIphoneXDevice ? 200 : 100, height),
        //     from, inputStyle);
        //
        // limit = GUI.TextArea(
        //     new Rect(Judge.IsIphoneXDevice ? 290 : 150, Judge.IsIphoneXDevice ? 140 : 0,
        //         Judge.IsIphoneXDevice ? 200 : 100, height), limit, inputStyle);
        //
        // userId = GUI.TextArea(new Rect(Judge.IsIphoneXDevice ? 520 : 270, Judge.IsIphoneXDevice ? 140 : 0, 380, height),
        //     userId, inputStyle);
        //
        // richKey = GUI.TextArea(
        //     new Rect(Judge.IsIphoneXDevice ? fx : 30, Judge.IsIphoneXDevice ? 960 : 7 * space,
        //         Judge.IsIphoneXDevice ? 200 : 100, height),
        //     richKey, inputStyle);
        //
        // richValue = GUI.TextArea(
        //     new Rect(Judge.IsIphoneXDevice ? 300 : 200, Judge.IsIphoneXDevice ? 960 : 7 * space,
        //         Judge.IsIphoneXDevice ? 200 : 100, height),
        //     richValue, inputStyle);

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 260 : space, 280, height), "添加好友", style))
        // {
        //     TapFriends.AddFriend(userId, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "添加好友成功";
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 380 : 2 * space, 280, height), "删除好友", style))
        // {
        //     TapFriends.DeleteFriend(userId, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "删除好友成功";
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 500 : 3 * space, 280, height), "好友列表", style))
        // {
        //     int handleFrom = int.Parse(from);
        //     int handleLimit = int.Parse(limit);
        //     TapFriends.GetFollowingList(handleFrom, false, handleLimit, (list, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             if (list.Count != 0)
        //             {
        //                 label = "获取好友列表成功: ";
        //                 foreach (TapUserRelationShip relation in list)
        //                 {
        //                     var richPresence = relation.richPresence.Aggregate("",
        //                         (current, richEntry) =>
        //                             current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));
        //
        //                     this.label = this.label + "userId：" + relation.userId +
        //                                  " name：" + relation.name +
        //                                  " avatar：" + relation.avatar +
        //                                  " gender：" + relation.gender +
        //                                  " mutualAttention：" + relation.mutualAttention +
        //                                  " onLine：" + relation.online +
        //                                  " time：" + relation.time +
        //                                  " relationship：" + relation.relationship +
        //                                  " rich_presence:" + richPresence
        //                         ;
        //                 }
        //             }
        //             else
        //             {
        //                 this.label = "获取好友列表为空";
        //             }
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 620 : 4 * space, 300, height), "互关好友列表", style))
        // {
        //     int handleFrom = int.Parse(from);
        //     int handleLimit = int.Parse(limit);
        //     TapFriends.GetFollowingList(handleFrom, true, handleLimit, (list, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             if (list.Count != 0)
        //             {
        //                 this.label = "获取互关好友列表成功: ";
        //                 foreach (TapUserRelationShip relation in list)
        //                 {
        //                     var richPresence = relation.richPresence.Aggregate("",
        //                         (current, richEntry) =>
        //                             current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));
        //
        //                     this.label = this.label + "userId：" + relation.userId +
        //                                  " name：" + relation.name +
        //                                  " avatar：" + relation.avatar +
        //                                  " gender：" + relation.gender +
        //                                  " onLine：" + relation.online +
        //                                  " time：" + relation.time +
        //                                  " mutualAttention：" + relation.mutualAttention +
        //                                  " relationship：" + relation.relationship +
        //                                  " rich_presence:" + richPresence
        //                         ;
        //                 }
        //             }
        //             else
        //             {
        //                 this.label = "获取互关好友列表为空";
        //             }
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 740 : 5 * space, 280, height), "获取粉丝列表", style))
        // {
        //     int handleFrom = int.Parse(from);
        //     int handleLimit = int.Parse(limit);
        //     TapFriends.GetFollowerList(handleFrom, handleLimit, (list, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             if (list.Count != 0)
        //             {
        //                 this.label = "获取粉丝列表成功: ";
        //                 foreach (TapUserRelationShip relation in list)
        //                 {
        //                     var richPresence = relation.richPresence.Aggregate("",
        //                         (current, richEntry) =>
        //                             current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));
        //
        //                     this.label = this.label + "userId：" + relation.userId +
        //                                  " name：" + relation.name +
        //                                  " avatar：" + relation.avatar +
        //                                  " gender：" + relation.gender +
        //                                  " onLine：" + relation.online +
        //                                  " time：" + relation.time +
        //                                  " mutualAttention：" + relation.mutualAttention +
        //                                  " relationship：" + relation.relationship +
        //                                  " rich_presence:" + richPresence
        //                         ;
        //                 }
        //             }
        //             else
        //             {
        //                 this.label = "获取粉丝列表为空";
        //             }
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 860 : 6 * space, 280, height), "拉黑用户", style))
        // {
        //     TapFriends.BlockUser(userId, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "拉黑用户成功";
        //         }
        //     });
        // }

        // if (GUI.Button(
        //     new Rect(Judge.IsIphoneXDevice ? 380 : 340, Judge.IsIphoneXDevice ? 620 : 4 * space, 280, height), "取消拉黑用户",
        //     style))
        // {
        //     TapFriends.UnblockUser(userId, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "取消拉黑用户成功";
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 740 : 5 * space, 280, height), "拉黑列表", style))
        // {
        //     int handleFrom = int.Parse(from);
        //     int handleLimit = int.Parse(limit);
        //     TapFriends.GetBlockList(handleFrom, handleLimit, (list, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             if (list.Count != 0)
        //             {
        //                 label = "获取拉黑列表成功";
        //                 foreach (TapUserRelationShip relation in list)
        //                 {
        //                     var richPresence = relation.richPresence.Aggregate("",
        //                         (current, richEntry) =>
        //                             current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));
        //
        //                     this.label = this.label + "userId：" + relation.userId +
        //                                  " name：" + relation.name +
        //                                  " avatar：" + relation.avatar +
        //                                  " gender：" + relation.gender +
        //                                  " onLine：" + relation.online +
        //                                  " time：" + relation.time +
        //                                  " mutualAttention：" + relation.mutualAttention +
        //                                  " relationship：" + relation.relationship +
        //                                  " rich_presence:" + richPresence
        //                         ;
        //                 }
        //             }
        //             else
        //             {
        //                 label = "获取拉黑列表为空";
        //             }
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 260 : space, 280, height), "搜索好友", style))
        // {
        //     TapFriends.SearchUser(userId, (relation, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "搜索用户成功";
        //             var richPresence = relation.richPresence.Aggregate("",
        //                 (current, richEntry) =>
        //                     current + ("key:" + richEntry.Key + " value:" + (string) richEntry.Value + "\n"));
        //
        //             this.label = this.label + "userId：" + relation.userId +
        //                          " name：" + relation.name +
        //                          " avatar：" + relation.avatar +
        //                          " gender：" + relation.gender +
        //                          " mutualAttention：" + relation.mutualAttention +
        //                          " relationship：" + relation.relationship +
        //                          " onLine：" + relation.online +
        //                          " time：" + relation.time +
        //                          " rich_presence:" + richPresence
        //                 ;
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 380 : 2 * space, 280, height), "好友邀请链接", style))
        // {
        //     TapFriends.GenerateFriendInvitation((invitationString, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "获取好友邀请链接成功: ";
        //             this.label = this.label + invitationString;
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 500 : 3 * space, 280, height), "分享好友邀请", style))
        // {
        //     TapFriends.SendFriendInvitation((isInvitation, error) =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"Error:{error.code} Description:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "分享好友邀请: ";
        //             this.label = this.label + (isInvitation ? "成功" : "失败");
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 860 : 6 * space, 180, height), "返回", style))
        // {
        //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        // }

        // if (GUI.Button(new Rect(fx, Judge.IsIphoneXDevice ? 1060 : 8 * space, 280, height), "设置富信息", style))
        // {
        //     TapFriends.SetRichPresence(richKey, richValue, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"设置富信息失败:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "设置富信息成功";
        //         }
        //     });
        // }

        // if (GUI.Button(new Rect(sx, Judge.IsIphoneXDevice ? 1060 : 8 * space, 280, height), "取消富信息", style))
        // {
        //     TapFriends.ClearRichPresence(richKey, error =>
        //     {
        //         if (error != null)
        //         {
        //             label = $"取消富信息失败:{error.errorDescription}";
        //         }
        //         else
        //         {
        //             label = "取消富信息成功";
        //         }
        //     });
        // }
    // }
}