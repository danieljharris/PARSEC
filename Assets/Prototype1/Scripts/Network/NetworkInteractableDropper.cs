using Fusion;
using Tilia.Interactions.Interactables.Interactables;

public class NetworkInteractableDropper : NetworkBehaviour
{
    // If interactable has been picked up by another player, drop it
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_Drop() => GetComponent<InteractableFacade>().UngrabAll();
}