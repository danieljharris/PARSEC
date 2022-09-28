using UnityEngine;

public class ResetObject : MonoBehaviour
{
    [SerializeField] ResettableObject Object;
    public void ResetTransform() => Object.ResetTransform();
}