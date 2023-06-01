using Fusion;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class NetworkPlayer : NetworkBehaviour
{
    // Disable game objects depending on current or network players
    [SerializeField] private GameObject[] _disableForLocalPlayer;
    [SerializeField] private GameObject[] _disableForNetworkPlayer;
    [SerializeField] private MonoBehaviour[] _disableScriptsForNetworkPlayer;
    [SerializeField] private UnityEvent onLocalPlayerJoined;
    [SerializeField] private UnityEvent onRemotePlayerJoined;
    [SerializeField] private GameObject[] _localAvatars;
    [SerializeField] private GameObject[] _networkAvatars;

    [Networked, Capacity(4)]
    private NetworkLinkedList<int> objectsToSpawn { get; }
        = MakeInitializer(new int[] { 0, 1, 2, 3 });

    public override void Spawned()
    {
        int index = Random.Range(0, objectsToSpawn.Count);
        int objectId = objectsToSpawn[index];
        objectsToSpawn.Remove(objectId);

        if (Object.HasInputAuthority)
        {
            // Spawned as own rig
            foreach (GameObject go in _disableForLocalPlayer) go.SetActive(false);
            onLocalPlayerJoined.Invoke();

            _localAvatars[objectId].gameObject.SetActive(true);
        }
        else
        {
            // Spawned as other player rig
            foreach (GameObject go in _disableForNetworkPlayer) go.SetActive(false);
            foreach (MonoBehaviour mb in _disableScriptsForNetworkPlayer) mb.enabled = false;
            onRemotePlayerJoined.Invoke();

            _networkAvatars[objectId].gameObject.SetActive(true);
        }

        DontDestroyOnLoad(gameObject);
    }
}