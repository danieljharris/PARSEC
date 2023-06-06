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