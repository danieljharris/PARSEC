using UnityEngine;

public static class Utility
{
    public static Vector3 ScaleAround(Transform target, Vector3 pivot, float newScale)
    {
        Vector3 difference = target.localPosition - pivot;
        float relativeScale = newScale / target.localScale.x;
        Vector3 newPosition = pivot + difference * relativeScale;
        return newPosition;
    }
}