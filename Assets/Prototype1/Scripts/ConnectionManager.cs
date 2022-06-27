using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public LineRenderer linePrefab;
    public Connection[] connections;

    // Start is called before the first frame update
    void Start()
    {
        // Creates a child object under the source for each connection
        for (int i = 0; i < connections.Length; i++)
            connections[i].lineRenderer = Instantiate(linePrefab, connections[i].source.transform);


        foreach (Connection connection in connections)
        {
            ConfigLine(connection);
            DrawLine(connection);
        }
    }

    void Update()
    {
        foreach (Connection connection in connections)
            DrawLine(connection);
    }

    private void ConfigLine(Connection connection)
    {
        ref LineRenderer line = ref connection.lineRenderer;

        Color colour = connection.colour;
        line.startColor = colour;
        line.endColor = colour;

        // Mesh mesh = new Mesh();
        // line.BakeMesh(mesh, true);
        // line.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

        // Set trackers so each node knows who it is connected to
        ConfigTracker(connection.source, connection.target, connection);
        ConfigTracker(connection.target, connection.source, connection);
    }
    private void ConfigTracker(GameObject node, GameObject connectedTo, Connection connection)
    {
        ConnectionHolder tracker = node.GetComponentInChildren<ConnectionHolder>();
        if (tracker == null) tracker = node.GetComponentInChildren<ConnectionHolder>();

        tracker.connections.Add((connectedTo, connection));
    }

    private void DrawLine(Connection connection)
    {
        GameObject source = connection.source;
        GameObject target = connection.target;
        LineRenderer lineRenderer = connection.lineRenderer;

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
        lineRenderer.SetPosition(0, center += (direction * offset));
        lineRenderer.SetPosition(1, center2 += (-direction * offset2));
    }
}