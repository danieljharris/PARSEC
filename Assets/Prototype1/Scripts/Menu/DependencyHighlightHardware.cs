using UnityEngine;

public class DependencyHighlightHardware : MonoBehaviour
{
    public Hardware hardware;

    NodeList nodeList;
    void Start()
    {
        nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");
    }

    public void addFilter() => nodeList.addFilter(hardware);
    public void removeFilter() => nodeList.removeFilter(hardware);
}