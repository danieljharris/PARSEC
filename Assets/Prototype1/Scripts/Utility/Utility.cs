using System;
using System.Collections;
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

    // public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    // {
    //     if (gameObject.TryGetComponent<T>())
    //         return gameObject.GetComponent<T>();
    //     else     
    //         return gameObject.AddComponent<T>() as T;
    // }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        if(gameObject.TryGetComponent<T>(out T t)) return t;
        else return gameObject.AddComponent<T>();
    }

    // public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    // {
    //     T component = gameObject.GetComponent<T>();
    //     if (component == null) component = gameObject.AddComponent<T>();
    //     return component;
    // }
}