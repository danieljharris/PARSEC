using Fusion;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Oculus.Avatar2;

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

    [SerializeField] private GameObject avatar;

    [Networked, Capacity(4)]
    private NetworkLinkedList<int> objectsToSpawn { get; }
        = MakeInitializer(new int[] { 0, 1, 2, 3 });

    public override void Spawned()
    {
        NetworkAvatar networkAvatar = GetComponent<NetworkAvatar>();

        int index = Random.Range(0, objectsToSpawn.Count);
        int objectId = objectsToSpawn[index];
        objectsToSpawn.Remove(objectId);

        // NetworkAvatar networkAvatar = avatar.GetComponent<NetworkAvatar>();
        // NetworkAvatarEntity avatarEntity = avatar.GetComponent<NetworkAvatarEntity>();
        // avatarEntity.avatarZipID = objectId.ToString();

        if (Object.HasInputAuthority)
        {
            // Spawned as own rig
            foreach (GameObject go in _disableForLocalPlayer) go.SetActive(false);
            onLocalPlayerJoined.Invoke();

            // networkAvatar.isLocal = true;
            // avatarEntity.isLocal = true;

            networkAvatar.isLocal = true;
            _localAvatars[objectId].gameObject.SetActive(true);
            networkAvatar.Avatar = _localAvatars[objectId].GetComponent<OvrAvatarEntity>();
        }
        else
        {
            // Spawned as other player rig
            foreach (GameObject go in _disableForNetworkPlayer) go.SetActive(false);
            foreach (MonoBehaviour mb in _disableScriptsForNetworkPlayer) mb.enabled = false;
            onRemotePlayerJoined.Invoke();


            // networkAvatar.isLocal = false;
            // avatarEntity.isLocal = false;

            networkAvatar.isLocal = false;
            _networkAvatars[objectId].gameObject.SetActive(true);
            networkAvatar.Avatar = _networkAvatars[objectId].GetComponent<OvrAvatarEntity>();
        }
        // avatar.SetActive(true);

        DontDestroyOnLoad(gameObject);
    }
}