using System;
using UnityEngine;

[Serializable]
public struct Connection
{
    public GameObject source;
    public GameObject target;
    public Color colour;
    
    [HideInInspector]
    public LineRenderer lineRenderer;
}