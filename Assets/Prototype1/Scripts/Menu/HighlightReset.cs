using UnityEngine;
using System.Collections.Generic;
using Tilia.Indicators.SpatialTargets;
using static HighlightType;

public class HighlightReset : MonoBehaviour
{
    public List<SpatialTargetFacade> filterButtons = new List<SpatialTargetFacade>();
    [SerializeField] private HighlightType resetType;

    void Start()
    {
        Presenter.onPresenterChanged += removeAllFilters;

        GameObject[] networks = GameObject.FindGameObjectsWithTag("Network");
        if (networks.Length == 0) Debug.Log("No network found");

        foreach (GameObject network in networks)
        {
            NodeList nodeList = network.GetComponent<NodeList>();
            if (nodeList == null) continue;
            
            switch (resetType)
            {
                case Ware:
                    nodeList.WareReset = this;
                    break;
                case Attack:
                    nodeList.AttackReset = this;
                    break;
            }
        }
    }
    
    public void removeAllFilters()
    {
        foreach (SpatialTargetFacade button in filterButtons)
            button.Deselect();
    }
}