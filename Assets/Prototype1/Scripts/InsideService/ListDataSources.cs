using TMPro;
using UnityEngine;

public class ListDataSources : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        foreach (DataSource dataSources in nodeSpecs.dataSources)
            text.text += dataSources.ToString() + "\n";
    }
}