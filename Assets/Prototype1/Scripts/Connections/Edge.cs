using UnityEngine;

public struct Edge
{
    public Edge(Node target, Color color, LineRenderer lineRenderer)
    {
        this.target = target;
        this.startingColor = color;
        this.lineRenderer = lineRenderer;
    }
    public Node target { get; }
    public Color startingColor { get; }
    public LineRenderer lineRenderer { get; set; }
}