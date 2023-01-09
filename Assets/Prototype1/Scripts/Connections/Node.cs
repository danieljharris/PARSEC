using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private bool highlighted = false;
    private Color currentHighlightColor = Color.clear;

    private List<Edge> edges = new List<Edge>();

    public HashSet<ConnectionGroup> connectionGroups { get; set; } = new HashSet<ConnectionGroup>(new ReferenceEqualityComparer<ConnectionGroup>());

    public bool HasTarget(Node target) => edges.Exists(edge => ReferenceEquals(edge.target, target));
    public void AddEdge(Edge newEdge) => edges.Add(newEdge);

    public void Highlight(Color color)
    {
        // If this node is already highlighted, return
        if (highlighted) return;
        highlighted = true;
        currentHighlightColor = color;

        // Get the visuals object from the node
        GameObject applyHighlightTo = transform.Find("Visuals").gameObject;

        // Add an outline component to the visuals object
        Outline outline = applyHighlightTo.GetOrAddComponent<Outline>();

        // Configure the outline component
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = color;
        outline.OutlineWidth = 5f;

        // Loop through all the edges
        for (int i = 0; i < edges.Count; i++)
        {
            // If the target node is also highlighted in the same color, change the edge color
            if(edges[i].target.currentHighlightColor == color)
            {
                edges[i].lineRenderer.startColor = color;
                edges[i].lineRenderer.endColor = color;
            }
        }
    }

    public void UnHighlight(Color color)
    {
        // If the node is not highlighted, or if the node is not highlighted in the specified color, then return.
        if (!highlighted || currentHighlightColor != color) return;

        // Get the object that has the outline effect and remove it. 
        GameObject applyHighlightTo = transform.Find("Visuals").gameObject;
        Outline outline = applyHighlightTo.GetComponent<Outline>();
        if (outline == null) return;

        // Set currentHighlightColor to clear and destroy the outline effect.
        currentHighlightColor = Color.clear;
        Destroy(outline);
        highlighted = false;

        // Loop through all edges connected to the node.
        foreach (Edge edge in edges)
        {
            // If the target node of the edge is highlighted in the specified color, then set the line color to the starting color.
            if (edge.target.currentHighlightColor == color)
            {
                edge.lineRenderer.startColor = edge.startingColor;
                edge.lineRenderer.endColor = edge.startingColor;
            }
        }
    }

    public NodeSpecs specs
    {
        get
        {
            return GetComponent<NodeSpecs>();
        }
        private set { }
    }
}