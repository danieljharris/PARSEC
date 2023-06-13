# Multiplayer Meta Avatar Example

PARSEC can be used as an example for how to implement the Meta Avatar SDK with Photon Fusion alongside VRTK v4 Tilia to create multiplayer VR avatars.

The pre-configured environment and components used to create a multiplayer VR avatars are discussed in this document.

<img height="200" src=https://github.com/danieljharris/PARSEC/assets/1362512/472c8438-10bb-4008-ab4f-8d013e32b318>

### Example Scene
Location: Assets/Prototype1/Scenes/Networking Example

This scene provides a barebones example of a multiplayer VR avatar application using the Meta Avatar SDK and Photon Fusion, using VRTK v4 Tilia for VR support.

The scene loads up to 4 players into a scene.

### Player Prefab
The player prefabs have 8 pre-configured meta avatars, 4 for local players and 4 for remote players. When a user spawns one of the 4 avatars are randomly chosen. The local avatars are set to "First Person" so that their head is made invisable to stop it getting in the way of the camera. The remote avatars are set to "Third Person" to make the avatar's head visable.

The chosen avatar gets set as the "OvrAvatarEntity Avatar" refered to in the code below.

Legs are disabled in this example but can be enabled with the following settings:
- Creation Info > Render Filters > Manifestation Flags = Full
- Active Manifestation = Full

### Code
This code is for the main componet that enabled multiplayer support for the Meta avatars using Photon Fusion

```csharp
using Fusion;
using UnityEngine;
using System.Collections.Generic;
using Oculus.Avatar2;

public class NetworkAvatar : NetworkBehaviour
{
    [SerializeField] public OvrAvatarEntity Avatar;
    [SerializeField] public bool isLocal = true;
    private float cycleStartTime_Local = 0;
    private float intervalToSendData_Local = 0.08f;
    private List<byte[]> streamedDataList_Remote = new List<byte[]>();

    void LateUpdate()
    {
        if (isLocal) LocalLateUpdate();
    }
    void Update()
    {
        if (!isLocal) RemoteUpdate();
    }

#region Local Functions
    private void LocalLateUpdate()
    {
        float elapsedTime = Time.time - cycleStartTime_Local;
        if (elapsedTime <= intervalToSendData_Local) return;

        RecordAndSendStreamData();
        cycleStartTime_Local = Time.time;
    }

    void RecordAndSendStreamData()
    {
        byte[] bytes = Avatar.RecordStreamData(Avatar.activeStreamLod);
        if (bytes == null) return;

        RPC_ReceiveStreamData(bytes);
    }
#endregion

#region Remote Functions
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_ReceiveStreamData(byte[] bytes)
    {
        streamedDataList_Remote.Add(bytes);
    }
    
    private void RemoteUpdate()
    {
        if (streamedDataList_Remote.Count == 0) return;

        byte[] firstBytesInList = streamedDataList_Remote[0];
        if (firstBytesInList != null)
        {
            Avatar.ApplyStreamData(firstBytesInList);
        }
        streamedDataList_Remote.RemoveAt(0);
    }
#endregion
}
```
