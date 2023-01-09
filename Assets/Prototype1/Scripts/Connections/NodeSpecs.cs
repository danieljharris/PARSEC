using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NodeSpecs : MonoBehaviour
{
    public List<Software> software = new List<Software>();
    public List<Hardware> hardware = new List<Hardware>();

    public void Highlight(Color color) => GetComponent<Node>().Highlight(color);
    public void UnHighlight(Color color) => GetComponent<Node>().UnHighlight(color);

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_Highlight(Color color) => GetComponent<Node>().Highlight(color);
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_UnHighlight(Color color) => GetComponent<Node>().UnHighlight(color);
}

public enum Software {
    Linux,
    Windows,
    Mac,
    CiscoIOS,
    IBMNetworkingOS,

    Nginx,
    Apache,
    
    MySQL,
    PostgreSQL,
    MongoDB,
    DynamoDB,

    PHP,
    Python,
    NodeJS,
    Java,
    CSharp,
    CPlusPlus,
    Go,
    Rust,

    Docker,
    Kubernetes,

    Ubuntu,
    CentOS,

    Vim,
    Emacs,
    Nano,
}

public enum Hardware {
    Intel,
    AMD,
    Nvidia,
    ARM,
    IBM,
    Cisco
}