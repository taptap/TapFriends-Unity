using System;
using UnityEngine;
using TapTap.Bootstrap;
using TapTap.Common;
using TapTap.Login;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    // Start is called before the first frame update

    //登录
    public Button login;

    //退出登录
    public Button loginout;

    //sessionToken
    public Button sessionToken;

    //用户详细信息
    public Button objectId;

    //篝火测试
    public Button bonfireTest;

    //文本
    public Text label;

    //返回
    public Button back;

    public Button guestLogin;

    void Start()
    {
        TapLogin.ChangeConfig(true, true);

        login.onClick.AddListener(onLoginClicked);
        loginout.onClick.AddListener(onLoginOutClicked);
        sessionToken.onClick.AddListener(GetSessionToken);
        objectId.onClick.AddListener(GetObjectId);
        bonfireTest.onClick.AddListener(onBonfireTestClicked);
        back.onClick.AddListener(onBackClicked);
        guestLogin.onClick.AddListener(GuestLogin);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private async void onLoginClicked()
    {
        try
        {
            var tdsUser = await TDSUser.LoginWithTapTap();
            label.text = $"login Success:{tdsUser}";
            Debug.Log($"login Success:{tdsUser}");
        }
        catch (Exception e)
        {
            if (e is TapException error)
            {
                label.text = $"Login Error:{error.code} message:{error.message}";
            }
            else
            {
                label.text = $"Login Error:{e}";
            }
            throw;
        }
    }

    private async void GuestLogin()
    {
        try
        {
            var tdsUser = await TDSUser.LoginAnonymously();
            label.text = $"Guest Login Success: {tdsUser}";
        }
        catch (Exception e)
        {
            label.text = $"Guest Login failed:{e.Message}";
        }
    }

    private void onLoginOutClicked()
    {
        TDSUser.Logout();
    }

    private async void GetSessionToken()
    {
        try
        {
            var tdsUser = await TDSUser.GetCurrent();
            label.text = $"sessionToken:{tdsUser.SessionToken}";
        }
        catch (Exception e)
        {
            label.text = $"sessionToken error:{e.Message}";
        }
    }

    private async void GetObjectId()
    {
        try
        {
            var tdsUser = await TDSUser.GetCurrent();
            label.text = $"ObjectId:{tdsUser.ObjectId}";
        }
        catch (Exception e)
        {
            label.text = $"ObjectId error:{e.Message}";
        }
    }

    private async void onBonfireTestClicked()
    {
        try
        {
            var test = await TapLogin.GetTestQualification();
            label.text = $"篝火测试:{test}";
        }
        catch (Exception e)
        {
            label.text = $"篝火测试 error:{e.Message}";
        }
    }

    private void onBackClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

    // private string label;

    // private void OnGUI()
    // {
    // var style = new GUIStyle(GUI.skin.button) {fontSize = 40};
    //
    // GUI.depth = 0;
    //
    // var labelStyle = new GUIStyle(GUI.skin.label)
    // {
    //     fontSize = 20
    // };
    //
    // GUI.Label(new Rect(400, 100, 400, 300), label, labelStyle);

    // if (GUI.Button(new Rect(60, 150, 180, 100), "登录", style))
    // {
    //     TapBootstrap.GetAccessToken((token, error) =>
    //     {
    //         if (token != null)
    //         {
    //             label = $"LoginSuccess:{token.ToJSON()}";
    //         }
    //         else
    //         {
    //             TapBootstrap.Login(LoginType.TAPTAP, new[] {"public_profile"});
    //         }
    //     });
    //
    //     TapLogin.GetProfile(profile => Debug.Log($"Profile:{profile.ToJson()}"));
    //     TapLogin.GetTapToken(token => Debug.Log($"TapLoToken:{token.ToJson()}"));
    // }

    // if (GUI.Button(new Rect(60, 300, 180, 100), "退出登录", style))
    // {
    //     TapBootstrap.Logout();
    // }

    // if (GUI.Button(new Rect(60, 450, 180, 100), "用户信息", style))
    // {
    //     TapBootstrap.GetUser((user, error) =>
    //     {
    //         label = user != null
    //             ? $"user:{user.ToJSON()}"
    //             : $"Error:{error?.code} Descrption:{error?.errorDescription}";
    //     });
    // }

    // if (GUI.Button(new Rect(60, 600, 260, 100), "用户详细信息", style))
    // {
    //     TapBootstrap.GetDetailUser((user, error) =>
    //     {
    //         label = user != null
    //             ? $"detailUser:{user.ToJSON()}"
    //             : $"Error:{error?.code} Descrption:{error?.errorDescription}";
    //     });
    // }

    // if (GUI.Button(new Rect(60, 750, 260, 100), "用户中心", style))
    // {
    // TapBootstrap.OpenUserCenter();
    // }

    // if (GUI.Button(new Rect(60, 900, 260, 100), "篝火测试", style))
    // {
    //     TapBootstrap.GetTestQualification((b, error) =>
    //     {
    //         label = $"篝火测试资格:{b} Error:{error?.code} Descrption:{error?.errorDescription}";
    //     });
    // }

    // if (GUI.Button(new Rect(60, 1050, 180, 100), "返回", style))
    // {
    //     UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    // }
    // }
}