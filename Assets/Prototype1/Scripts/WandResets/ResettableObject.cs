using Tilia.Interactions.Interactables.Interactables;
using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Transform OriginalParent;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;
    private Vector3 OriginalScale;

    void Start()
    {
        OriginalParent = transform.parent;
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalScale = transform.localScale;
    }
    public void ResetTransform()
    {
        GetComponent<InteractableFacade>().Ungrab();

        transform.parent = OriginalParent;
        transform.position = OriginalPosition;
        transform.rotation = OriginalRotation;
        transform.localScale = OriginalScale;
    }
}