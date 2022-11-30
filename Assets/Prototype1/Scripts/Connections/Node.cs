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
        if (highlighted) return;
        highlighted = true;
        currentHighlightColor = color;

        GameObject applyHighlightTo = transform.Find("Visuals").gameObject;
        Outline outline = applyHighlightTo.GetOrAddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = color;
        outline.OutlineWidth = 5f;

        for (int i = 0; i < edges.Count; i++)
        {
            if(edges[i].target.currentHighlightColor == color)
            {

                edges[i].lineRenderer.startColor = color;
                edges[i].lineRenderer.endColor = color;
            }
        }
    }

    public void UnHighlight(Color color)
    {
        if (!highlighted) return;
        if (currentHighlightColor != color) return;
        
        GameObject applyHighlightTo = transform.Find("Visuals").gameObject;
        Outline outline = applyHighlightTo.GetComponent<Outline>();
        if (outline == null) return;

        currentHighlightColor = Color.clear;
        Destroy(outline);
        highlighted = false;

        foreach (Edge edge in edges)
        {
            if(edge.target.currentHighlightColor == color)
            {
                edge.lineRenderer.startColor = edge.startingColor;
                edge.lineRenderer.endColor = edge.startingColor;
            }
        }
    }
}