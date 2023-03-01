using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField]
    private bool isPresenter = false;
    public Color color;
    private Node nodeInCollision = null;


    // Highlight when entering a node
    void OnTriggerEnter(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        nodeInCollision = node;

        Highlight(node, color);
    }
    private void Highlight(Node node, Color color)
    {
        if (isPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_Highlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.Highlight(color);
    }


    // Unhighlight when leaving the node
    void OnTriggerExit(Collider other)
    {
        Node node = other?.gameObject?.GetComponent<Node>();
        if (node == null) return;

        nodeInCollision = null;

        UnHighlight(node, color);
    }
    private void UnHighlight(Node node, Color color)
    {
        if (isPresenter)
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.RPC_UnHighlight(color);
        else
            foreach (ConnectionGroup conGroup in node.connectionGroups)
                conGroup.UnHighlight(color);
    }

    // Set the presenter status
    public void SetPresenter(bool presenterStatus)
    {
        isPresenter = presenterStatus;

        if(nodeInCollision == null) return;

        if(isPresenter)
            foreach (ConnectionGroup conGroup in nodeInCollision.connectionGroups)
                conGroup.RPC_Highlight(color);
        else
            foreach (ConnectionGroup conGroup in nodeInCollision.connectionGroups)
                conGroup.RPC_UnHighlight(color);
    }
}