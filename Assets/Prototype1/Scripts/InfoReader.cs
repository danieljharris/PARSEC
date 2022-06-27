using TMPro;
using UnityEngine;

public class InfoReader : MonoBehaviour
{
    public TextMeshPro infoPanelText;
    void OnTriggerEnter(Collider other)
    {
        ObjectInfo tracker = other?.gameObject?.GetComponent<ObjectInfo>();
        if (tracker == null) return;

        // tracker.Highlight(colour, colourID);
    }
    void OnTriggerExit(Collider other)
    {
        ObjectInfo tracker = other?.gameObject?.GetComponent<ObjectInfo>();
        if (tracker == null) return;

        // tracker.UnHighlight(colour);
    }
}
