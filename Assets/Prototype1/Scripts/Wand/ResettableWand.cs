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
    public void WandReset()
    {
        InteractableFacade facade = GetComponent<InteractableFacade>();
        if (facade == null) return;
        facade.Ungrab();

        transform.parent = OriginalParent;
        transform.localPosition = OriginalPosition;
        transform.localRotation = OriginalRotation;
        transform.localScale = OriginalScale;

        WandPickup pickup = GetComponent<WandPickup>();
        if (pickup == null) return;
        pickup.AttachedToMenu = true;
    }
}