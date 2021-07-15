using System;
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

    public void OnLoginSuccess(AccessToken token)
    {
        label = $"Login Success:{token.ToJson()}";
    }

    public void OnLoginCancel()
    {
        label = "Login Cancel";
    }

    public void OnLoginError(TapError error)
    {
        label = error != null ? $"Login Error:{error.code} desc:{error.errorDescription}" : "Login Error";
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

        GUI.Label(new Rect(400, 100, 400, 300), label, labelStyle);

        if (GUI.Button(new Rect(60, 150, 180, 100), "登录", style))
        {
            Login();
        }

        if (GUI.Button(new Rect(60, 300, 180, 100), "退出登录", style))
        {
            // TapBootstrap.Logout();
            TDSUser.Logout();
        }

        if (GUI.Button(new Rect(60, 450, 180, 100), "AccessToken", style))
        {
            GetAccessToken();
        }

        if (GUI.Button(new Rect(60, 600, 260, 100), "Profile", style))
        {
            GetProfile();
        }

        if (GUI.Button(new Rect(60, 750, 260, 100), "Remote Profile", style))
        {
            GetRemoteProfile();
        }

        if (GUI.Button(new Rect(60, 900, 260, 100), "篝火测试", style))
        {
            GetTestQualification();
            // TapBootstrap.GetTestQualification((b, error) =>
            // {
            //     label = $"篝火测试资格:{b} Error:{error?.code} Descrption:{error?.errorDescription}";
            // });
        }

        if (GUI.Button(new Rect(60, 1050, 180, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }

    private static async void GetTestQualification()
    {
        try
        {
            var boolean = await TapLogin.GetTestQualification();
            Debug.Log($"TestQualification:{boolean}");
        }
        catch (Exception e)
        {
            Debug.Log($"{e}");
        }
    }

    private static async void Login()
    {
        try
        {
            var tdsUser = await TDSUser.LoginWithTapTap();
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get AccessToken exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private static async void GetAccessToken()
    {
        try
        {
            var accessToken = await TapLogin.GetAccessToken();
            Debug.Log($"accessToken:{accessToken.ToJson()}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get AccessToken exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private static async void GetProfile()
    {
        try
        {
            var accessToken = await TapLogin.GetProfile();
            Debug.Log($"accessToken:{accessToken.ToJson()}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get profile exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }

    private static async void GetRemoteProfile()
    {
        try
        {
            var accessToken = await TapLogin.FetchProfile();
            Debug.Log($"remote profile:{accessToken.ToJson()}");
        }
        catch (Exception e)
        {
            if (e is TapException tapError)
            {
                Debug.Log($"get remote profile exception:{tapError.code} message:{tapError.message}");
            }

            // ignored
        }
    }
}