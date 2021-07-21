using System;
using System.Reflection;
using LeanCloud.Storage;
using TapTap.Bootstrap;
using UnityEngine;
using TapTap.Common;
using TapTap.Login;
using AccessToken = TapTap.Login.AccessToken;

public class LoginScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TapLogin.ChangeConfig(true, true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string label;

    private void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.button) {fontSize = 40};

        GUI.depth = 0;

        var labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20
        };

        GUI.Label(new Rect(500, 100, 400, 300), label, labelStyle);

        if (GUI.Button(new Rect(60, 150, 300, 100), "登录", style))
        {
            Login();
        }


        if (GUI.Button(new Rect(60, 300, 300, 100), "退出登录", style))
        {
            label = $"logout";
            TDSUser.Logout();
        }

        if (GUI.Button(new Rect(60, 450, 300, 100), "SessionToken", style))
        {
            GetSessionToken();
        }

        if (GUI.Button(new Rect(60, 600, 300, 100), "Get ObjectId", style))
        {
            GetObjectId();
        }

        if (GUI.Button(new Rect(60, 750, 300, 100), "Get CurrentUser", style))
        {
            GetCurrentUser();
        }

        if (GUI.Button(new Rect(60, 900, 300, 100), "篝火测试", style))
        {
            GetTestQualification();
        }

        if (GUI.Button(new Rect(60, 1050, 400, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }

        if (GUI.Button(new Rect(60, 1200, 400, 100), "游客登陆", style))
        {
            LoginAnonymously();
        }
    }

    private async void LoginAnonymously()
    {
        try
        {
            var tdsUser = await TDSUser.LoginAnonymously();
            label = $"LoginAnonymously Success:{Json.Serialize(tdsUser)}";
            Debug.Log($"LoginAnonymously Success:{Json.Serialize(tdsUser)}");
        }
        catch (Exception)
        {
            //
        }
    }

    private async void GetTestQualification()
    {
        try
        {
            var boolean = await TapLogin.GetTestQualification();
            label = $"test:{boolean}";
            Debug.Log($"TestQualification:{boolean}");
        }
        catch (Exception e)
        {
            Debug.Log($"{e}");
        }
    }

    private async void Login()
    {
        try
        {
            var tdsUser = await TDSUser.LoginWithTapTap();
            label = $"Login Success:{Json.Serialize(tdsUser)}";
            Debug.Log($"Login Success:{Json.Serialize(tdsUser)}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"glogin exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private async void GetSessionToken()
    {
        try
        {
            var tdsUser = await TDSUser.GetCurrent();
            label = $"sessionToken:{tdsUser?.SessionToken}";
            Debug.Log($"sessionToken:{tdsUser?.SessionToken}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get sessionToken exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private async void GetObjectId()
    {
        // var info = typeof(LCUser).GetField("currentUser").GetValue(null) as LCUser;
        
        try
        {
            var tdsUser = await TDSUser.GetCurrent();
            label = $"objectId:{tdsUser?.ObjectId}";
            Debug.Log($"objectId:{tdsUser?.ObjectId}");

            // var info = typeof(LCUser).GetField("currentUser").GetValue(null) as LCUser;
            //
            // var property = typeof(LCUser).GetProperty("currentUser")?.GetValue(null, null) as LCUser;
            //
            // Debug.Log($"info:{info?.SessionToken} +  objectId: +{info?.ObjectId}");
            // Debug.Log($"property:{property?.SessionToken} +  property: +{property?.ObjectId}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get objectId exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private async void GetCurrentUser()
    {
        try
        {
            var tdsUser = await TDSUser.GetCurrent();
            label = $"currentUser:{Json.Serialize(tdsUser)}";
            Debug.Log($"currentUser:{Json.Serialize(tdsUser)}");
        }
        catch (Exception e)
        {
            // 
        }
    }
}