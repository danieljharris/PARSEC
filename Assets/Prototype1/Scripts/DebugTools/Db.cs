using UnityEngine;

// Debug Tools
public class Db : MonoBehaviour
{
    public static GameObject CreateSphere(Vector3 position, Color color)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = Vector3.one * 0.05f;
        sphere.GetComponent<Renderer>().material.color = color;
        return sphere;
    }
    public static GameObject CreateSphere(Vector3 position, Color color, Vector3 size)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = size;
        sphere.GetComponent<Renderer>().material.color = color;
        return sphere;
    }
}
