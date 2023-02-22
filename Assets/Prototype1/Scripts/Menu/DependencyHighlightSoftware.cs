using UnityEngine;

public class DependencyHighlightSoftware : MonoBehaviour
{
    public Software software;

    NodeList nodeList;
    void Start()
    {
        nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");
    }

    public void addFilter() => nodeList.addFilter(software);
    public void removeFilter() => nodeList.removeFilter(software);
}