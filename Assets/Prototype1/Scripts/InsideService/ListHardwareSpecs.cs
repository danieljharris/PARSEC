using TMPro;
using UnityEngine;

public class ListHardwareSpecs : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Hardware hardware in nodeSpecs.hardware)
            text.text += hardware.ToString() + "\n";
    }
}
