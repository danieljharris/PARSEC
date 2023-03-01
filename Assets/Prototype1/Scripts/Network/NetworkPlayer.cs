using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    // Disable game objects depending on current or network players
    [SerializeField] private GameObject[] _disableForLocalPlayer;
    [SerializeField] private GameObject[] _disableForNetworkPlayer;
    [SerializeField] private MonoBehaviour[] _disableScriptsForNetworkPlayer;
    [SerializeField] private Presenter presenter;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            // Spawned as own rig
            foreach (GameObject go in _disableForLocalPlayer) go.SetActive(false);
        }
        else
        {
            // Spawned as other player rig
            foreach (GameObject go in _disableForNetworkPlayer) go.SetActive(false);
            foreach (MonoBehaviour mb in _disableScriptsForNetworkPlayer) mb.enabled = false;
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority) Runner.Despawn(Object);
    }

    // [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    // private void RPC_SetPresenter(bool presenterStatus)
    // {
    //     isPresenter = presenterStatus;

    //     // As own player's instance of head is disabled, these changes will not effect local player
    //     foreach (GameObject go in _presenterOnly)
    //         go.SetActive(presenterStatus);
    // }
}