using UnityEngine;
using System.Collections.Generic;

public abstract class MenuHighlighting : MonoBehaviour
{
    [SerializeField] protected Presenter presenter;
    protected List<NodeList> nodeLists = new List<NodeList>();
    
    void Start()
    {
        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        if (networks.Length == 0) Debug.Log("No network found");

        foreach (GameObject network in networks)
        {
            NodeList nodeList = network.GetComponent<NodeList>();
            if (nodeList == null) Debug.Log("No nodeList found");
            else nodeLists.Add(nodeList);
        }
    }

    public abstract void addFilter();
    public abstract void removeFilter();
}