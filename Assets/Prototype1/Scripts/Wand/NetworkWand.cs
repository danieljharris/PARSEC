using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

public class NetworkWand : NetworkBehaviour
{
    [SerializeField] private Presenter presenter;

    [SerializeField] private GameObject[] wands;

    public void Show()
    {
        if (presenter.IsPresenter) RPC_Show();
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_Show()
    {
        foreach (GameObject wand in wands)
        {
            wand.SetActive(true);
        }
    }
    public void Hide()
    {
        if (presenter.IsPresenter) RPC_Hide();
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = false)]
    public void RPC_Hide()
    {
        foreach (GameObject wand in wands)
        {
            wand.SetActive(false);
        }
    }
}
