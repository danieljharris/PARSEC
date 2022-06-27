using System.Collections.Generic;
using UnityEngine;

public class ConnectionHolder : MonoBehaviour
{
    public List<(GameObject, Connection)> connections = new List<(GameObject, Connection)>();
    private Color highlightedColour = Color.clear;

    public void Highlight(Color colour, int colourID)
    {
        if (highlightedColour != Color.clear) return;

        if (highlightedColour == colour) return;
        highlightedColour = colour;

        cakeslice.Outline outline = gameObject.GetComponentInChildren<cakeslice.Outline>();
        if (outline.eraseRenderer == false) return;
        outline.eraseRenderer = false;
        outline.color = colourID;

        foreach ((GameObject node, Connection con) in connections)
        {
            node.GetComponent<ConnectionHolder>()?.Highlight(colour, colourID);

            con.lineRenderer.startColor = colour;
            con.lineRenderer.endColor = colour;
        }
    }

    public void UnHighlight(Color colour)
    {
        if (highlightedColour != colour) return;

        if (highlightedColour == Color.clear) return;
        highlightedColour = Color.clear;

        cakeslice.Outline outline = gameObject.GetComponentInChildren<cakeslice.Outline>();
        outline.eraseRenderer = true;

        foreach ((GameObject node, Connection con) in connections)
        {
            node.GetComponent<ConnectionHolder>()?.UnHighlight(colour);

            con.lineRenderer.startColor = con.colour;
            con.lineRenderer.endColor = con.colour;
        }
    }
}