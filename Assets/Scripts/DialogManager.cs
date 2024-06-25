using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Dialog currentDialog;
    private string dialogFileName="dialog";

    void Start()
    {
        LoadDialog();
    }

    void LoadDialog()
    {
        TextAsset dialogText = Resources.Load<TextAsset>(dialogFileName);
        if (dialogText != null)
        {
            currentDialog = JsonUtility.FromJson<Dialog>(dialogText.text);
        }
        else
        {
            Debug.LogError("Dialog file not found in Resources.");
        }
    }

    public DialogNode GetNodeByID(int id)
    {
        return currentDialog.nodes.Find(node => node.nodeID == id);
    }
}
