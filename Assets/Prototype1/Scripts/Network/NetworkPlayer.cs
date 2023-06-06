using UnityEngine;
using UnityEngine.Events;
using Fusion;
using Oculus.Avatar2;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private UnityEvent onLocalPlayerJoined;
    [SerializeField] private UnityEvent onRemotePlayerJoined;
    [SerializeField] private GameObject[] _localAvatars;
    [SerializeField] private GameObject[] _networkAvatars;
    [Networked, SerializeField] private int _avatarId { get; set; } = -1;

    public override void Spawned()
    {
        NetworkAvatar networkAvatar = GetComponent<NetworkAvatar>();
        bool isLocal = Object.HasInputAuthority;
        GameObject avatar = GetAvatar(isLocal);

        SpawnPlayer(networkAvatar, avatar, isLocal);
        
        DontDestroyOnLoad(gameObject);
    }

    private GameObject GetAvatar(bool isLocal)
    {
        GameObject[] avatars = isLocal ? _localAvatars : _networkAvatars;
        if(_avatarId != -1) return avatars[_avatarId];

        if (isLocal && _avatarId == -1)
        {
            _avatarId = Random.Range(0, 3);

            return avatars[_avatarId];
        }

        Debug.LogError("Avatar ID not set, retrying...");

        return GetAvatar(isLocal);
    }

    private void SpawnPlayer(NetworkAvatar networkAvatar, GameObject avatar, bool isLocal)
    {
        UnityEvent playerJoined = isLocal ? onLocalPlayerJoined : onRemotePlayerJoined;
        playerJoined.Invoke();

        avatar.SetActive(true);

        networkAvatar.isLocal = isLocal;
        networkAvatar.Avatar = avatar.GetComponent<OvrAvatarEntity>();
    }
}