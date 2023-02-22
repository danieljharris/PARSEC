using TMPro;
using UnityEngine;

public class ListHardwareSpecs : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        foreach (Hardware hardware in nodeSpecs.hardware)
            text.text += "- " + hardware.ToString() + "\n";
    }
}