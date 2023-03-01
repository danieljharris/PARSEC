using UnityEngine;
using System.Collections.Generic;
using Tilia.Indicators.SpatialTargets;

public class HighlightReset : MonoBehaviour
{
    public List<SpatialTargetFacade> filterButtons = new List<SpatialTargetFacade>();
    [SerializeField] private Presenter presenter;

    void Start() => presenter.onPresenterChanged += removeAllFilters;
    
    public void removeAllFilters()
    {
        foreach (SpatialTargetFacade button in filterButtons)
            button.Deselect();
    }
}