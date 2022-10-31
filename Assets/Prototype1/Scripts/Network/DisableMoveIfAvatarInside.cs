using UnityEngine;
using Fusion;
using Tilia.Interactions.Interactables.Interactables;

public class DisableMoveIfAvatarInside : NetworkBehaviour
{
    // Enable Interactable for all Users
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_EnableInteractable() => GetComponent<InteractableFacade>().enabled = true;

    // Disable Interactable for all Users
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_DisableInteractable() => GetComponent<InteractableFacade>().enabled = false;

    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Collider>().tag != "Network Service") return;
        
        col.GetComponent<RPC_Disableable>.
        
    }
}