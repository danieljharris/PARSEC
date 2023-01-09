using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public bool isPresenter = false;
    public Color color;

    void OnTriggerEnter(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        if (isPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_Highlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.Highlight(color);
    }
    void OnTriggerExit(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        if (isPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_UnHighlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.UnHighlight(color);
    }
}