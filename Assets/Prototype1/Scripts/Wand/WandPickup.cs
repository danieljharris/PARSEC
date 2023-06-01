using Fusion;
using Tilia.Interactions.Interactables.Interactors;
using UnityEngine;
using UnityEngine.Events;

public class WandPickup : NetworkBehaviour
{
    [SerializeField] private Presenter presenter;
    public bool AttachedToMenu = true;
    public UnityEvent OnPickup = new UnityEvent();
    public void OnGrab(InteractorFacade interactor)
    {
        if (AttachedToMenu)
        {
            this.transform.parent = null;
            this.transform.localScale = interactor.transform.lossyScale;
            AttachedToMenu = false;
            OnPickup.Invoke();

            if (presenter.IsPresenter) RPC_OnGrab(interactor.transform.lossyScale);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_OnGrab(Vector3 scale)
    {
        this.transform.parent = null;
        this.transform.localScale = scale;
        AttachedToMenu = false;
        OnPickup.Invoke();
    }


    public void Hide()
    {
        if (AttachedToMenu)
        {
            gameObject.SetActive(false);
            if (presenter.IsPresenter) RPC_Hide();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_Hide()
    {
        gameObject.SetActive(false);
    
    }
}