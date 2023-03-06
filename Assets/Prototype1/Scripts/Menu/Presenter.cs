using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class Presenter : NetworkBehaviour
{
    [SerializeField] GameObject[] _presenterOnly;
    [SerializeField] private static HashSet<Presenter> allPlayers = new HashSet<Presenter>();

    void Start()
    {
        allPlayers.Add(this);
    }

    public static bool IsAnyPresenter {get; set;} = false;

    // Only used by local player
    public void SetPresenter(bool presenterStatus)
    {
        if (IsPresenter == presenterStatus) return;

        if (presenterStatus) IsAnyPresenter = true;
        else if (IsPresenter) IsAnyPresenter = false; // Existing presenter is being disabled

        IsPresenter = presenterStatus;

        RPC_RemoteSetPresenter(IsPresenter, IsAnyPresenter);

        if(IsPresenter)
            foreach (Presenter player in allPlayers)
            {
                if (player == this) continue;
                player.IsPresenter = false;
                player.RPC_RemoteSetPresenter(false);
            }
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    private void RPC_RemoteSetPresenter(bool presenterStatus)
    {
        IsPresenter = presenterStatus;
        foreach (GameObject go in _presenterOnly) go.SetActive(presenterStatus);
    }
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    private void RPC_RemoteSetPresenter(bool presenterStatus, bool anyPresenterStatus)
    {
        IsPresenter = presenterStatus;
        IsAnyPresenter = anyPresenterStatus;
        foreach (GameObject go in _presenterOnly) go.SetActive(presenterStatus);
    }

    [Networked(OnChanged = "OnPresenterChanged")] public NetworkBool IsPresenter { get; set; } = false;
    public static void OnPresenterChanged(Changed<Presenter> changed) => onPresenterChanged.Invoke();
    public static event UnityAction onPresenterChanged;
}