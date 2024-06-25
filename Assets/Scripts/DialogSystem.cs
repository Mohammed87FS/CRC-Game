using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    private bool gameStarted = false;

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<MapGenerator>().StartGame();
        gameObject.SetActive(false); 
        
    }
}
