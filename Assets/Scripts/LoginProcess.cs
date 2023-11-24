public class LoginProcess : UIManager
{
    public void Login()
    {
        if (CheckNickname())
        {
            SceneChange("LobbyScene");
        }
    }


    public bool CheckNickname()
    {
        string name = ES3.LoadString("Nickname","");
        if(name == "" || name == null)
        {
            gameObject.SetActive(true);
            return false;
        }
        return true;
    }
}
