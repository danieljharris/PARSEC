using UnityEngine;
using Fusion;

public class RPC_Disableable : NetworkBehaviour
{
    // Enable Interactable for all Users
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_Enable<Component>() where Component: MonoBehaviour =>
        (GetComponent<Component>() as MonoBehaviour).enabled = true;
        

    // Disable Interactable for all Users
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_Disable<Component>() where Component: MonoBehaviour =>
        (GetComponent<Component>() as MonoBehaviour).enabled = false;
       
}