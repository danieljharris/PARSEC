using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private Presenter presenter;
    public Color color;
    private Node nodeInCollision = null;

    public void IsEnabled(bool value) => isEnabled = value;

    // Highlight when entering a node
    void OnTriggerEnter(Collider other)
    {
        if(!isEnabled) return;

        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        nodeInCollision = node;

        Highlight(node, color);
    }
    private void Highlight(Node node, Color color)
    {
        if (presenter.IsPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_Highlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.Highlight(color);
    }


    // Unhighlight when leaving the node
    void OnTriggerExit(Collider other)
    {
        if(!isEnabled) return;

        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        nodeInCollision = null;

        UnHighlight(node, color);
    }
    private void UnHighlight(Node node, Color color)
    {
        if (presenter.IsPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_UnHighlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.UnHighlight(color);
    }

    public void UnHighlightAll()
    {
        if (nodeInCollision == null) return;
        UnHighlight(nodeInCollision, color);
    }
}