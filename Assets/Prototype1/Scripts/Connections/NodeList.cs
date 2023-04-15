using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HighlightType;

public class NodeList : MonoBehaviour
{
    public List<NodeSpecs> nodeSpecs = new List<NodeSpecs>();

    public Color filterColor = Color.white;
    [SerializeField] private List<Software> softwareFilters = new List<Software>();
    [SerializeField] private List<Hardware> hardwareFilters = new List<Hardware>();
    [SerializeField] private List<AttackType> attackTypeFilters = new List<AttackType>();
    public HighlightReset WareReset;
    public HighlightReset AttackReset;
    [SerializeField] private HighlightType highlightType = NotSet;

    private int filterCountWare()
    {
        return softwareFilters.Count + hardwareFilters.Count + attackTypeFilters.Count;
    }
    private int filterCountArrack()
    {
        return attackTypeFilters.Count;
    }

    private void applyFilterWare(bool isPresenter)
    {
        if(highlightType == Attack)
        {
            // If using ware highlighting, disable attack highlighting
            AttackReset.removeAllFilters();
            attackTypeFilters = new List<AttackType>();
            clearHighlighting(isPresenter, true);
        }
        highlightType = Ware;

        StartCoroutine(applyFilterWareForEach(isPresenter));
    }
    private IEnumerator applyFilterWareForEach(bool isPresenter)
    {
        yield return new WaitForSeconds(0.1f);

        foreach (NodeSpecs specs in nodeSpecs)
            specs.ApplyFilter(filterColor, isPresenter, softwareFilters, hardwareFilters);
    }
    private void applyFilterAttack(bool isPresenter)
    {
        if(highlightType == Ware)
        {
            // If using ware highlighting, disable attack highlighting
            WareReset.removeAllFilters();
            softwareFilters = new List<Software>();
            hardwareFilters = new List<Hardware>();
            clearHighlighting(isPresenter, true);
        }
        highlightType = Attack;

        StartCoroutine(applyFilterAttackForEach(isPresenter));
    }
    private IEnumerator applyFilterAttackForEach(bool isPresenter)
    {
        yield return new WaitForSeconds(0.1f);

        foreach (NodeSpecs specs in nodeSpecs)
            specs.ApplyFilter(isPresenter, attackTypeFilters);
    }
    private void clearHighlighting(bool isPresenter, bool force = false)
    {
        highlightType = NotSet;

        if (force)
        {
            if (isPresenter)
            {
                foreach (NodeSpecs specs in nodeSpecs)
                    specs.RPC_UnHighlight();
            }
            else
            {
                foreach (NodeSpecs specs in nodeSpecs)
                    specs.UnHighlight();
            }
        }
        else
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
    }
    private IEnumerator clearHighlightingCoroutine(bool isPresenter, bool force = false)
    {
        yield return new WaitForSeconds(0.1f);
        clearHighlighting(isPresenter, force);
    }

    // Software
    public void addFilter(Software software, bool isPresenter)
    {
        softwareFilters.Add(software);
        applyFilterWare(isPresenter);
    }
    public void removeFilter(Software software, bool isPresenter)
    {
        softwareFilters.Remove(software);
        if(filterCountWare() != 0)
        {
            applyFilterWare(isPresenter);
        }
        else
        {
            //Sleep for 1 second to allow previous highlighting to clear via applyFilterWare
            StartCoroutine(clearHighlightingCoroutine(isPresenter));
        }
    }

    // Hardware
    public void addFilter(Hardware hardware, bool isPresenter)
    {
        hardwareFilters.Add(hardware);
        applyFilterWare(isPresenter);
    }
    public void removeFilter(Hardware hardware, bool isPresenter)
    {
        hardwareFilters.Remove(hardware);

        if(filterCountWare() != 0)
        {
            applyFilterWare(isPresenter);
        }
        else
        {
            //Sleep for 1 second to allow previous highlighting to clear via applyFilterWare
            StartCoroutine(clearHighlightingCoroutine(isPresenter));
        }
    }

    // AttackTypes
    public void addFilter(AttackType attackTypes, bool isPresenter)
    {
        attackTypeFilters.Add(attackTypes);
        applyFilterAttack(isPresenter);
    }
    public void removeFilter(AttackType attackTypes, bool isPresenter)
    {
        attackTypeFilters.Remove(attackTypes);

        if(filterCountArrack() != 0) applyFilterAttack(isPresenter);
        else clearHighlighting(isPresenter, true);


        if(filterCountArrack() != 0)
        {
            applyFilterAttack(isPresenter);
        }
        else
        {
            //Sleep for 1 second to allow previous highlighting to clear via applyFilterWare
            StartCoroutine(clearHighlightingCoroutine(isPresenter, true));
        }
    }
}

enum HighlightType
{
    NotSet,
    Ware,
    Attack
}