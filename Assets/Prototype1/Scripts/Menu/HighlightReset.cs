using UnityEngine;
using System.Collections.Generic;
using Tilia.Indicators.SpatialTargets;
using static HighlightType;

public class HighlightReset : MonoBehaviour
{
    public List<SpatialTargetFacade> filterButtons = new List<SpatialTargetFacade>();
    [SerializeField] private Presenter presenter;
    [SerializeField] private HighlightType resetType;

    void Start()
    {
        presenter.onPresenterChanged += removeAllFilters;

        NodeList nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");

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
    
    public void removeAllFilters()
    {
        foreach (SpatialTargetFacade button in filterButtons)
            button.Deselect();
    }
}