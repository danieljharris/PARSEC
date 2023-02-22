using UnityEngine;

public class DependencyHighlightAttackType : MonoBehaviour
{
    public AttackType attackType;

    NodeList nodeList;
    void Start()
    {
        nodeList = GameObject.FindWithTag("Network")?.GetComponent<NodeList>();
        if (nodeList == null) Debug.Log("Network/NodeList not found");
    }

    public void addFilter() => nodeList.addFilter(attackType);
    public void removeFilter() => nodeList.removeFilter(attackType);
}