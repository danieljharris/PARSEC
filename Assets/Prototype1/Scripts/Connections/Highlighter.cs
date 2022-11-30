using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Color color;

    void OnTriggerEnter(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        foreach (ConnectionGroup conGroup in node.connectionGroups)
            conGroup.Highlight(color);
    }
    void OnTriggerExit(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        foreach (ConnectionGroup conGroup in node.connectionGroups)
            conGroup.UnHighlight(color);
    }
}