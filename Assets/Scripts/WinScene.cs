using UnityEngine;

public class WinScene : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        GameManager.instance.LoadMainMenu();
    }
}
