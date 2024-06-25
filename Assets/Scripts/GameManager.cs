using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

 
    public void LoadGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

   
    public void QuitGame()
    {
        Application.Quit();
    }
}

