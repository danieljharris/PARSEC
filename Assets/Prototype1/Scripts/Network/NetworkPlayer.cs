using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    [SerializeField] GameObject[] _disableForLocalPlayer;
    [SerializeField] GameObject[] _disableForNetworkPlayer;

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
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority) Runner.Despawn(Object);
    }
}