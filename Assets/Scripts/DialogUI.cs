using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public Text dialogText;
    public Transform responseButtonParent;
    public GameObject responseButtonPrefab;

    private DialogManager dialogManager;
    private DialogSystem dialogSystem;
    private int currentNodeID;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        dialogSystem = FindObjectOfType<DialogSystem>();
        ShowNode(0);
    }

    public void ShowNode(int nodeID)
    {
        DialogNode node = dialogManager.GetNodeByID(nodeID);
        dialogText.text = node.dialogText;
        ClearResponseButtons();

        foreach (var response in node.responses)
        {
            GameObject button = Instantiate(responseButtonPrefab, responseButtonParent);
            button.GetComponentInChildren<Text>().text = response.responseText;
            button.GetComponent<Button>().onClick.AddListener(() => OnResponseSelected(response.nextNodeID));
        }

        currentNodeID = nodeID;
    }

    void ClearResponseButtons()
    {
        foreach (Transform child in responseButtonParent)
        {
            Destroy(child.gameObject);
        }
    }

    void OnResponseSelected(int nextNodeID)
    {
        if (nextNodeID == -1)
        {
            EndDialog();
        }
        else
        {
            ShowNode(nextNodeID);
        }
    }

    void EndDialog()
    {
        var mapGenerator = FindObjectOfType<MapGenerator>();
        if (mapGenerator != null)
        {
            mapGenerator.StartGame();
        }
        else
        {
            Debug.LogError("MapGenerator not found. Ensure it is added to the scene.");
        }

        gameObject.SetActive(false);
    }
}
