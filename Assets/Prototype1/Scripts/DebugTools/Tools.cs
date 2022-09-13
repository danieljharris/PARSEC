using UnityEngine;

// Debug Tools
public class Tools : MonoBehaviour
{
    public static GameObject Sphere(Vector3 position, Color color) =>
        Sphere(position, color, Vector3.one * 0.05f);
    public static GameObject Sphere(Vector3 position, Color color, Vector3 size) =>
        CreatePrimitive(PrimitiveType.Sphere, position, color, size);

    public static GameObject Cube(Vector3 position, Color color) =>
        Cube(position, color, Vector3.one * 0.05f);
    public static GameObject Cube(Vector3 position, Color color, Vector3 size) =>
        CreatePrimitive(PrimitiveType.Cube, position, color, size);

    
    public static GameObject CreatePrimitive(PrimitiveType type, Vector3 position, Color color, Vector3 size)
    {
        GameObject sphere = GameObject.CreatePrimitive(type);
        sphere.transform.position = position;
        sphere.transform.localScale = size;
        sphere.GetComponent<Renderer>().material.color = color;
        return sphere;
    }
}