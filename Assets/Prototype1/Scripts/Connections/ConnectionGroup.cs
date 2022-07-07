using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionGroup : MonoBehaviour
{
    [Serializable]
    public struct ConnectionInfo
    {
        public GameObject source;
        public GameObject target;
        public Color color;
    }
    public LineRenderer linePrefab;
    public ConnectionInfo[] connections;

    private HashSet<Node> nodes = new HashSet<Node>(new ReferenceEqualityComparer<Node>());
    private List<(GameObject, GameObject, LineRenderer)> nodePairs = new List<(GameObject, GameObject, LineRenderer)>();

    void Start()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            (Node source, Node target) = CreateNodes(connections[i].source, connections[i].target);
            nodes.Add(source);
            nodes.Add(target);

            LineRenderer line = CreateEdges(source, target, connections[i].color, linePrefab);
            if (line == null) continue;
            
            ConfigLine(line, connections[i].color);

            nodePairs.Add((connections[i].source, connections[i].target, line));
        }
    }
    
    void Update()
    {
        foreach ((GameObject source, GameObject target, LineRenderer line) in nodePairs)
            DrawLine(source, target, line);
    }

    private (Node source, Node target) CreateNodes(GameObject source, GameObject target)
    {
        Node sourceNode = source.GetComponent<Node>();
        if (sourceNode == null) sourceNode = source.AddComponent<Node>();
        sourceNode.connectionGroups.Add(this);

        Node targetNode = target.GetComponent<Node>();
        if (targetNode == null) targetNode = target.AddComponent<Node>();
        targetNode.connectionGroups.Add(this);

        return (sourceNode, targetNode);
    }
    private LineRenderer CreateEdges(Node source, Node target, Color color, LineRenderer linePrefab)
    {
        if (!source.HasTarget(target))
        {
            LineRenderer line = Instantiate(linePrefab, source.transform);

            source.AddEdge(new Edge(target, color, line));
            target.AddEdge(new Edge(source, color, line));

            return line;
        }
        else if (!target.HasTarget(source))
        {
            Debug.LogError("Empty edge detected!");
        }

        return null;
    }

    private void ConfigLine(LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;

        // Mesh mesh = new Mesh();
        // line.BakeMesh(mesh, true);
        // line.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void DrawLine(GameObject source, GameObject target, LineRenderer line)
    {
        Renderer rend = source.GetComponentInChildren<Renderer>();
        Renderer rend2 = target.GetComponentInChildren<Renderer>();

        Vector3 center = rend.bounds.center;
        Vector3 center2 = rend2.bounds.center;
        Vector3 direction = (center2 - center).normalized;

        float average(Vector3 vec) => (vec.x + vec.y + vec.z) / 3;

        float offset = average(rend.bounds.size) / 4;
        float offset2 = average(rend2.bounds.size) / 4;

        // Set the start and end points of the line
        //  with offset from the center of the collider
        line.SetPosition(0, center += (direction * offset));
        line.SetPosition(1, center2 += (-direction * offset2));
    }

    public void Highlight(Color color, int colorID)
    {
        foreach (Node node in nodes)
            node.Highlight(color, colorID);
    }
    public void UnHighlight(Color color)
    {
        foreach (Node node in nodes)
            node.UnHighlight(color);
    }
}