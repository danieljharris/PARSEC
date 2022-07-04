using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Color color;
    public int colorID;

    void OnTriggerEnter(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        foreach (ConnectionGroup conGroup in node.connectionGroups)
            conGroup.Highlight(color, colorID);
    }
    void OnTriggerExit(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        foreach (ConnectionGroup conGroup in node.connectionGroups)
            conGroup.UnHighlight(color);
    }
}