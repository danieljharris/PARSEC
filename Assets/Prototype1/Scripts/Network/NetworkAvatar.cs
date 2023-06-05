using Fusion;
using UnityEngine;
using System.Collections.Generic;
using Oculus.Avatar2;

public class NetworkAvatar : NetworkBehaviour
{
    [SerializeField] public OvrAvatarEntity Avatar;
    [SerializeField] public bool isLocal = true;
    private List<byte[]> streamedDataList = new List<byte[]>();
    private float cycleStartTime = 0;
    private float intervalToSendData = 0.08f;

    void Update()
    {
        if (!isLocal) RemoteUpdate();
    }

    void LateUpdate()
    {
        if (isLocal) LocalLateUpdate();
    }

#region Local
    private void LocalLateUpdate()
    {
        Debug.Log("LocalLateUpdate");

        float elapsedTime = Time.time - cycleStartTime;
        if (elapsedTime <= intervalToSendData) return;

        RecordAndSendStreamData();
        cycleStartTime = Time.time;
    }

    void RecordAndSendStreamData()
    {
        Debug.Log("Recording stream data");
        
        byte[] bytes = Avatar.RecordStreamData(Avatar.activeStreamLod);
        if (bytes == null) return;

        Debug.Log("Sending stream data");
        RPC_ReceiveStreamData(bytes);
    }
#endregion

#region Remote
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_ReceiveStreamData(byte[] bytes)
    {
        Debug.Log("Receiving stream data---------------------------------------");
        if(streamedDataList == null) streamedDataList = new List<byte[]>();
        Debug.Log("Adding stream data to list");
        streamedDataList.Add(bytes);
        Debug.Log("Stream data added to list");
    }
    private void RemoteUpdate()
    {
        Debug.Log("RemoteUpdate");
        if (streamedDataList.Count == 0) return;

        Debug.Log("Applying stream data");

        byte[] firstBytesInList = streamedDataList[0];
        Debug.Log("First bytes in list: " + firstBytesInList);
        if (firstBytesInList != null)
        {
            Debug.Log("Applying stream data");
            Avatar.ApplyStreamData(firstBytesInList);
        }
        Debug.Log("Removing stream data from list");
        streamedDataList.RemoveAt(0);
        Debug.Log("Stream data removed from list");
    }
#endregion
}