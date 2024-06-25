using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.LoadGameScene();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
