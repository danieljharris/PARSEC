using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public List<NodeSpecs> nodeSpecs = new List<NodeSpecs>();

    public Color filterColor = Color.red;
    private List<Software> softwareFilters = new List<Software>();
    private List<Hardware> hardwareFilters = new List<Hardware>();
    private List<AttackType> attackTypeFilters = new List<AttackType>();

    private int filterCount()
    {
        return softwareFilters.Count + hardwareFilters.Count + attackTypeFilters.Count;
    }

    private void applyFilter(bool isPresenter)
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if(specs.ContainsWare(softwareFilters, hardwareFilters))
                    specs.RPC_Highlight(filterColor);
                else
                    specs.RPC_UnHighlight(filterColor);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if(specs.ContainsWare(softwareFilters, hardwareFilters))
                    specs.Highlight(filterColor);
                else
                    specs.UnHighlight(filterColor);
        }
    }
    private void clearFilterHighlighting(bool isPresenter)
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
    public void addFilter(Software software, bool isPresenter)
    {
        softwareFilters.Add(software);

        applyFilter(isPresenter);
    }
    public void removeFilter(Software software, bool isPresenter)
    {
        softwareFilters.Remove(software);

        if(filterCount() != 0) applyFilter(isPresenter);
        else clearFilterHighlighting(isPresenter);
    }

    // Hardware
    public void addFilter(Hardware hardware, bool isPresenter)
    {
        hardwareFilters.Add(hardware);

        applyFilter(isPresenter);
    }
    public void removeFilter(Hardware hardware, bool isPresenter)
    {
        hardwareFilters.Remove(hardware);

        if(filterCount() != 0) applyFilter(isPresenter);
        else clearFilterHighlighting(isPresenter);
    }

    // AttackTypes
    public void addFilter(AttackType attackTypes, bool isPresenter)
    {
        attackTypeFilters.Add(attackTypes);

        applyFilter(isPresenter);
    }
    public void removeFilter(AttackType attackTypes, bool isPresenter)
    {
        attackTypeFilters.Remove(attackTypes);

        if(filterCount() != 0) applyFilter(isPresenter);
        else clearFilterHighlighting(isPresenter);
    }
}