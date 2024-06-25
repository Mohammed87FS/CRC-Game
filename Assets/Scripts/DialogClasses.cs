using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialog
{
    public List<DialogNode> nodes;
}

[System.Serializable]
public class DialogNode
{
    public int nodeID;
    public string dialogText;
    public List<DialogResponse> responses;
}

[System.Serializable]
public class DialogResponse
{
    public string responseText;
    public int nextNodeID;
}
