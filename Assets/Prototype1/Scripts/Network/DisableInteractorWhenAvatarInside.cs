using UnityEngine;
using Fusion;
using Tilia.Interactions.Interactables.Interactors.ComponentTags;

public class DisableInteractorWhenAvatarInside : NetworkBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Collider>().tag == "Player")
            RPC_RemoveInteractor();
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.GetComponent<Collider>().tag == "Player")
            RPC_AddInteractor();
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    private void RPC_AddInteractor()
    {
        if (GetComponent<AllowInteractorCollisionTag>() == null)
        {
            gameObject.AddComponent<AllowInteractorCollisionTag>();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    private void RPC_RemoveInteractor()
    {
        Destroy(GetComponent<AllowInteractorCollisionTag>());
    }
}