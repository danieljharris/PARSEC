using UnityEngine;

public class DependencyHighlightSoftware : MonoBehaviour
{
    public Software software;
    public Color color;

    NodeList nodeList;
    void Start()
    {
        nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");
    }

    public void Highlight() => nodeList.Highlight(software, color);
    public void UnHighlight() => nodeList.UnHighlight(software, color);
}