using TMPro;
using UnityEngine;

public class LoginProcess : UIManager
{
    public TextMeshProUGUI limitCharactersText;
    public TextMeshProUGUI inputNickname;
    public TextMeshProUGUI nickname;

    public GameObject popupUI;

    const int limitCharacters = 10;

    public void Login()
    {
        if (CheckNickname())
        {
            SceneChange("LobbyScene");
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public bool CheckNickname()
    {
        string name = ES3.LoadString("Nickname", "");
        if (name == "" || name == null)
        {
            return false;
        }
        return true;
    }

    public void CreateNickname()
    {
        //Debug.Log(inputNickname.text.Length);
        if (2 < inputNickname.text.Length && inputNickname.text.Length <= limitCharacters+1)
        {
            nickname.text = inputNickname.text;
            popupUI.SetActive(true);
            return;
        }
        
        limitCharactersText.color = Color.red;
    }

    public void YesButton()
    {
        ES3.Save("Nickname", inputNickname.text);
        Login();
    }

    public void NoButton()
    {
        popupUI.SetActive(false);
    }



}
