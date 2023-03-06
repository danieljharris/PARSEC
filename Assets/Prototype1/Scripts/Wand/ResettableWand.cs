using Fusion;
using Tilia.Interactions.Interactables.Interactables;
using UnityEngine;

public class ResettableWand : MonoBehaviour
{
    private Transform OriginalParent;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;
    private Vector3 OriginalScale;
    [SerializeField] private Presenter presenter;

    void Start()
    {
        OriginalParent = transform.parent;
        OriginalPosition = transform.position;
        OriginalPosition = transform.localPosition;
        OriginalRotation = transform.localRotation;
        OriginalScale = transform.localScale;

        Presenter.onPresenterChanged += WandReset;
    }

    public void WandReset()
    {
        Local_WandReset();
        if (presenter.IsPresenter) RPC_WandReset();
    }

    private void Local_WandReset()
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

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    private void RPC_WandReset() => Local_WandReset();
}