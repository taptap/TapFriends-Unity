using TapBootstrap;
using UnityEngine;

public class LoginScene : MonoBehaviour, ITapLoginResultListener, ITapUserStatusChangedListener
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string label;

    private bool isCN = true;

    private bool isCorner = true;

    public void OnLoginSuccess(AccessToken token)
    {
        label = $"Login Success:{token.ToJSON()}";
    }

    public void OnLoginCancel()
    {
        label = "Login Cancel";
    }

    public void OnLoginError(TapError error)
    {
        if (error != null)
        {
            label = $"Login Error:{error.code} desc:{error.errorDescription}";
        }
        else
        {
            label = "Login Error";
        }
    }

    public void OnLogout(TapError error)
    {
        if (error != null)
        {
            label = $"OnLogout Error:{error.code} desc:{error.errorDescription}";
        }
        else
        {
            label = "OnLogout Error";
        }
    }

    public void OnBind(TapError error)
    {
        if (error != null)
        {
            label = $"OnBind Error:{error.code} desc:{error.errorDescription}";
        }
        else
        {
            label = "OnBind Error";
        }
    }

    private void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20
        };

        GUI.depth = 0;

        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20
        };

        GUIStyle myToggleStyle = new GUIStyle(GUI.skin.toggle)
        {
            fontSize = 20
        };

        GUI.Label(new Rect(400, 600, 400, 300), label, myLabelStyle);

        GUI.Toggle(new Rect(50, 50, 100, 30), isCN, "国内", myToggleStyle);

        GUI.Toggle(new Rect(50, 100, 100, 30), isCorner, "圆角", myToggleStyle);

        if (GUI.Button(new Rect(50, 200, 200, 60), "初始化", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.Init(new TapConfig("KFV9Pm9ojdmWkkRJeb", false));
        }

        if (GUI.Button(new Rect(50, 300, 200, 60), "带参初始化", myButtonStyle))
        {
        }

        if (GUI.Button(new Rect(50, 400, 200, 60), "注册回调", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.RegisterLoginResultListener(this);
            TapBootstrap.TapBootstrap.RegisterUserStatusChangedListener(this);
        }

        if (GUI.Button(new Rect(50, 500, 200, 60), "开始登陆", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.Login(LoginType.TAPTAP, new[]
            {
                "public_profile"
            });
        }

        if (GUI.Button(new Rect(50, 600, 200, 60), "获取token", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.GetAccessToken((accessToken, error) =>
            {
                if (accessToken != null)
                {
                    Debug.Log("accessToken:" + accessToken.ToJSON());
                    this.label = accessToken.ToJSON();
                }
                else
                {
                    this.label = "accessToken is Null,Please login first!";
                }
            });
        }

        if (GUI.Button(new Rect(300, 50, 200, 60), "获取UserInfo", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.GetUser((user, error) =>
            {
                if (user != null)
                {
                    label = $"user:{user.ToJSON()}";
                }
                else if (error != null)
                {
                    label = $"error:{error.code} desc:{error.errorDescription}";
                }
            });
        }

        if (GUI.Button(new Rect(300, 150, 200, 60), "退出登录", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.Logout();
        }

        if (GUI.Button(new Rect(300, 250, 200, 60), "UserDetail", myButtonStyle))
        {
            TapBootstrap.TapBootstrap.GetDetailUser((user, error) =>
            {
                if (user != null)
                {
                    label = $"user:{user.ToJSON()} \n isMomentEnable:{user.userCenterEntry.isMomentEnabled}";
                }
                else if (error != null)
                {
                    label = $"error:{error.code} desc:{error.errorDescription}";
                }
            });
        }
    }
}