using Fusion;
using UnityEngine;
using UnityEngine.Events;
public class NetworkPlayer : NetworkBehaviour
{
    // Disable game objects depending on current or network players
    [SerializeField] private GameObject[] _disableForLocalPlayer;
    [SerializeField] private GameObject[] _disableForNetworkPlayer;
    [SerializeField] private MonoBehaviour[] _disableScriptsForNetworkPlayer;
    [SerializeField] private UnityEvent onLocalPlayerJoined;
    [SerializeField] private UnityEvent onRemotePlayerJoined;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            // Spawned as own rig
            foreach (GameObject go in _disableForLocalPlayer) go.SetActive(false);
            onLocalPlayerJoined.Invoke();
        }
        else
        {
            // Spawned as other player rig
            foreach (GameObject go in _disableForNetworkPlayer) go.SetActive(false);
            foreach (MonoBehaviour mb in _disableScriptsForNetworkPlayer) mb.enabled = false;
            onRemotePlayerJoined.Invoke();
        }

        DontDestroyOnLoad(gameObject);
    }
}