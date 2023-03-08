using TMPro;
using UnityEngine;

public class TMPChangeColour : MonoBehaviour
{
    public TextMeshPro txt;

    public void SetColour(TMP_ColorGradient colour)
    {
        txt.color = colour.topLeft;
    }
}