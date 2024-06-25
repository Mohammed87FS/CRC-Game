using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public void Retry()
    {
        GameManager.instance.LoadGameScene();
    }

    public void ReturnToMainMenu()
    {
        GameManager.instance.LoadMainMenu();
    }
}
