using UnityEngine;
using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;

public class NetworkSpawnRig : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPlayer _playerPrefab;
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Vector3 spawnPosition = gameObject.transform.position;
        Quaternion spawnRotation = gameObject.transform.rotation;
        PlayerRef player = runner.LocalPlayer;
        runner.Spawn(_playerPrefab, spawnPosition, spawnRotation, player);
    }

#region INetworkRunnerCallbacks
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
    public void OnDisconnectedFromServer(NetworkRunner runner) {}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}
    public void OnInput(NetworkRunner runner, NetworkInput input) {}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}
#endregion
}