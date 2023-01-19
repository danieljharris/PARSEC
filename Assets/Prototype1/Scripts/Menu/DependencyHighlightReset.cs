using UnityEngine;
using System.Collections.Generic;
using Tilia.Indicators.SpatialTargets;

public class DependencyHighlightReset : MonoBehaviour
{
    public List<SpatialTargetFacade> filterButtons = new List<SpatialTargetFacade>();
    
    public void removeAllFilters()
    {
        foreach (SpatialTargetFacade button in filterButtons)
            button.Deselect();
    }
}