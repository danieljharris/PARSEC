using TMPro;
using UnityEngine;

public class ListSoftwareSpecs : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        foreach (Software software in nodeSpecs.software)
            text.text += software.ToString() + "\n";
    }
}