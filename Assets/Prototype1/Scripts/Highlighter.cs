using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public Color colour;
    public int colourID;
    void OnTriggerEnter(Collider other)
    {
        ConnectionHolder tracker = other?.gameObject?.GetComponent<ConnectionHolder>();
        if (tracker == null) return;

        tracker.Highlight(colour, colourID);
    }
    void OnTriggerExit(Collider other)
    {
        ConnectionHolder tracker = other?.gameObject?.GetComponent<ConnectionHolder>();
        if (tracker == null) return;

        tracker.UnHighlight(colour);
    }
}