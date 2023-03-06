using UnityEngine;

public abstract class MenuHighlighting : MonoBehaviour
{
    [SerializeField] protected Presenter presenter;
    protected NodeList nodeList;
    
    void Start()
    {
        nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");
    }

    public abstract void addFilter();
    public abstract void removeFilter();
}