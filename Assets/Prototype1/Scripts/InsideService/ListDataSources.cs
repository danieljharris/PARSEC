using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ListDataSources : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        foreach (DataSource dataSource in nodeSpecs.dataSources)
        {
            string source = dataSource.ToString();
            
            // Add space before capital letters
            source = string.Concat(source.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

            text.text += "- " + source + "\n";
        }
    }
}