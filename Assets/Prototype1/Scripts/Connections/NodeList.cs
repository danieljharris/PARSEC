using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public bool isPresenter = false;
    public List<NodeSpecs> nodeSpecs = new List<NodeSpecs>();

    public Color filterColor = Color.red;
    private List<Software> softwareFilters = new List<Software>();
    private List<Hardware> hardwareFilters = new List<Hardware>();

    private int filterCount()
    {
        return softwareFilters.Count + hardwareFilters.Count;
    }

    private bool hasSpecs(NodeSpecs specs, List<Software> softwareFilters, List<Hardware> hardwareFilters)
    {
        return softwareFilters.All(specs.software.Contains) &&
               hardwareFilters.All(specs.hardware.Contains); 
    }
    private void applyFilter()
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if(hasSpecs(specs, softwareFilters, hardwareFilters))
                    specs.RPC_Highlight(filterColor);
                else
                    specs.RPC_UnHighlight(filterColor);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if(hasSpecs(specs, softwareFilters, hardwareFilters))
                    specs.Highlight(filterColor);
                else
                    specs.UnHighlight(filterColor);
        }
    }
    private void clearFilterHighlighting()
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                specs.RPC_UnHighlight(filterColor);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                specs.UnHighlight(filterColor);
        }
    }

    // Software
    public void addFilter(Software software)
    {
        softwareFilters.Add(software);

        applyFilter();
    }
    public void removeFilter(Software software)
    {
        softwareFilters.Remove(software);

        if(filterCount() != 0) applyFilter();
        else clearFilterHighlighting();
    }

    // Hardware
    public void addFilter(Hardware hardware)
    {
        hardwareFilters.Add(hardware);

        applyFilter();
    }
    public void removeFilter(Hardware hardware)
    {
        hardwareFilters.Remove(hardware);

        if(filterCount() != 0) applyFilter();
        else clearFilterHighlighting();
    }
}