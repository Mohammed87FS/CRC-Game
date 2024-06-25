using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text dialogText;
    public Button okButton;
    public Button startGameButton;
    public GameObject dialogPanel;

    private List<string> dialogs;
    private int currentDialogIndex = 0;
    private bool isGameStarted = false;

    void Start()
    {
        LoadDialogs();
        ShowDialog();

        okButton.onClick.AddListener(OnOkButtonClicked);
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    void LoadDialogs()
    {
        TextAsset dialogFile = Resources.Load<TextAsset>("dialog");
        if (dialogFile != null)
        {
            DialogData dialogData = JsonUtility.FromJson<DialogData>(dialogFile.text);
            dialogs = new List<string>(dialogData.dialogs);
        }
        else
        {
            Debug.LogError("Dialog file not found!");
            dialogs = new List<string>();
        }
    }

    void ShowDialog()
    {
        if (currentDialogIndex < dialogs.Count)
        {
            dialogText.text = dialogs[currentDialogIndex];
        }
      
    }

    void OnOkButtonClicked()
    {
        currentDialogIndex++;
        ShowDialog();
    }

    void OnStartGameButtonClicked()
    {
        isGameStarted = true;
        dialogPanel.SetActive(false);

     
        FindObjectOfType<MapGenerator>().StartGame();
    }

    [System.Serializable]
    public class DialogData
    {
        public string[] dialogs;
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}

