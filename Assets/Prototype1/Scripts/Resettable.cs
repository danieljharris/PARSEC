using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
    public void ResetTransform()
    {
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
    }
}