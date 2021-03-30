using TapBootstrap;
using UnityEngine;

public class LoginScene : MonoBehaviour
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

    private void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.button) {fontSize = 40};

        GUI.depth = 0;

        var labelStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20
        };

        GUI.Label(new Rect(400, 1500, 400, 300), label, labelStyle);

        if (GUI.Button(new Rect(60, 150, 180, 100), "登录", style))
        {
            TapBootstrap.TapBootstrap.GetAccessToken((token, error) =>
            {
                if (token != null)
                {
                    label = $"LoginSuccess:{token.ToJSON()}";
                }
                else
                {
                    TapBootstrap.TapBootstrap.Login(LoginType.TAPTAP, new[] {"public_profile"});
                }
            });
        }

        if (GUI.Button(new Rect(60, 300, 180, 100), "退出登录", style))
        {
            TapBootstrap.TapBootstrap.Logout();
        }

        if (GUI.Button(new Rect(60, 450, 180, 100), "用户信息", style))
        {
            TapBootstrap.TapBootstrap.GetUser((user, error) =>
            {
                label = user != null
                    ? $"user:{user.ToJSON()}"
                    : $"Error:{error.code} Descrption:{error.errorDescription}";
            });
        }

        if (GUI.Button(new Rect(60, 600, 260, 100), "用户详细信息", style))
        {
            TapBootstrap.TapBootstrap.GetDetailUser((user, error) =>
            {
                label = user != null
                    ? $"detailUser:{user.ToJSON()}"
                    : $"Error:{error.code} Descrption:{error.errorDescription}";
            });
        }

        if (GUI.Button(new Rect(60, 750, 180, 100), "返回", style))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}