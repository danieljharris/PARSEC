using Tilia.Interactions.Interactables.Interactables;
using UnityEngine;

public class ResettableWand : MonoBehaviour
{
    private Transform OriginalParent;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;
    private Vector3 OriginalScale;

    void Start()
    {
        OriginalParent = transform.parent;
        OriginalPosition = transform.position;
        OriginalPosition = transform.localPosition;
        OriginalRotation = transform.localRotation;
        OriginalScale = transform.localScale;
    }
    public void Reset()
    {
        GetComponent<InteractableFacade>().Ungrab();

        transform.parent = OriginalParent;
        transform.localPosition = OriginalPosition;
        transform.localRotation = OriginalRotation;
        transform.localScale = OriginalScale;

        GetComponent<WandPickup>().AttachedToMenu = true;
    }
}