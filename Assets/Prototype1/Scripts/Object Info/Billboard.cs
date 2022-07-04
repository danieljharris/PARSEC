using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera;
    void Start() => _camera = Camera.main;
    void LateUpdate() => transform.LookAt(_camera.transform);
}