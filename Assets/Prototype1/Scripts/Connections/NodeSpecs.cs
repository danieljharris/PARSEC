using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;

using static Detectability;
using System;

// C# 9.0 workaround for Unity .NET 5
namespace System.Runtime.CompilerServices { internal static class IsExternalInit {}}

public class NodeSpecs : NetworkBehaviour
{
    public List<Software>   software    = new List<Software>();
    public List<Hardware>   hardware    = new List<Hardware>();
    public List<DataSource> dataSources = new List<DataSource>();

    public Detectability[] AttackTypeDetectability {get;} = new Detectability[11];

    private static List<Detectability> NetworkLogsAttackTypes = new List<Detectability> {
        None,
        Full,
        Full,
        Partial,
        Partial,
        Full,
        Full,
        Partial,
        Partial,
        Full,
        Full
    };
    private static List<Detectability> ApplicationLogsAttackTypes = new List<Detectability> {
        None,
        Full,
        None,
        Partial,
        Partial,
        None,
        Full,
        Partial,
        Full,
        None,
        Full
    };
    private static List<Detectability> SystemEventLogsAttackTypes = new List<Detectability> {
        Full,
        None,
        None,
        Partial,
        Partial,
        None,
        None,
        Partial,
        Full,
        Full,
        Full
    };
    private static List<Detectability> GeolocationAttackTypes = new List<Detectability> {
        None,
        Full,
        Full,
        None,
        None,
        Full,
        Full,
        None,
        None,
        Full,
        Full
    };
    private static List<Detectability> ThreatProtectionApplicationsAttackTypes = new List<Detectability> {
        Full,
        Full,
        Full,
        Partial,
        Partial,
        Full,
        Partial,
        Full,
        Full,
        Full,
        Partial
    };

    private static List<List<Detectability>> AttackLookup =
        new List<List<Detectability>> {
            NetworkLogsAttackTypes,
            ApplicationLogsAttackTypes,
            SystemEventLogsAttackTypes,
            GeolocationAttackTypes,
            ThreatProtectionApplicationsAttackTypes
        };

    void Start()
    {
        // Fills list of attack type detectability from the data sources that are used
        // E.g. if NetworkLogs and Geolocation are used, then the list will be
        // a combination of NetworkLogsAttackTypes and GeolocationAttackTypes
        // where the highest detectability is used
        foreach (DataSource dataSource in dataSources)
        {
            List<Detectability> detectabilityList = AttackLookup[(int)dataSource];
            for (int i = 0; i < detectabilityList.Count; i++)
            {
                if ((int)detectabilityList[i] > (int)AttackTypeDetectability[i])
                {
                    AttackTypeDetectability[i] = detectabilityList[i];
                }
            }
        }
    }

    private Detectability GetAttackTypeDetectability(AttackType attackType)
    {
        return AttackTypeDetectability[(int)attackType];
    }

    public void ApplyFilter(Color filterColor, bool isPresenter, List<Software> software, List<Hardware> hardware)
    {
        Action filter = (isPresenter, Contains(software, hardware)) switch
        {
            (true, true) => () => RPC_Highlight(filterColor),
            (true, false) => () => RPC_UnHighlight(filterColor),
            (false, true) => () => Highlight(filterColor),
            (false, false) => () => UnHighlight(filterColor)
        };
        filter();
    }
    public void ApplyFilter(bool isPresenter, List<AttackType> attackTypes)
    {
        Detectability lowest = Full;

        foreach (AttackType attackType in attackTypes)
        {
            if (GetAttackTypeDetectability(attackType) < lowest)
            {
                lowest = GetAttackTypeDetectability(attackType);
            }
        }

        if (lowest == Full)
            Highlight(Color.green, true);
        else if (lowest == Partial)
            Highlight(Color.yellow, true);
        else
            Highlight(Color.red, true);
    }

    private bool Contains(List<Software> software, List<Hardware> hardware)
    {
        return software.All(this.software.Contains) &&
               hardware.All(this.hardware.Contains); 
    }

    private Node GetNode()
    {
        Node node = GetComponent<Node>();
        if (node != null) return node;
        else return transform.Find("MeshContainer").Find("Service Specs").gameObject.GetComponent<Node>();
    }

    public void Highlight(Color color, bool force = false) => GetNode().Highlight(color, force);
    public void UnHighlight(Color color) => GetNode().UnHighlight(color);
    public void UnHighlight() => GetNode().UnHighlight();

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_Highlight(Color color, bool force = false) => GetNode().Highlight(color, force);

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_UnHighlight(Color color) => GetNode().UnHighlight(color);
    
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_UnHighlight() => GetNode().UnHighlight();
}

public enum Software {
    Linux,
    Windows,
    // Mac,
    CiscoIOS,
    // IBMNetworkingOS,

    Nginx,
    // Apache,
    
    MySQL,
    // PostgreSQL,
    // MongoDB,
    // DynamoDB,

    // PHP,
    // Python,
    // NodeJS,
    Java,
    // CSharp,
    // CPlusPlus,
    // Go,
    // Rust,

    // Docker,
    // Kubernetes,

    // Ubuntu,
    // CentOS,
    // Debian,
}

public enum Hardware {
    Intel,
    AMD,
    Nvidia,
    ARM,
    IBM,
    Cisco
}

public enum Detectability {
    None,
    Partial,
    Full
} 

public enum AttackType {
    PhysicalAttacks,
    Reconnaissance,
    SocialEngineering,
    InsiderAttacks,
    SupplyChainAttacks,
    ManInTheMiddle,
    DenialOfService,
    Misconfiguration,
    SoftwareVulnerabilities,
    Malware,
    PasswordAttacks
}

public enum DataSource {
    NetworkLogs,
    ApplicationLogs,
    SystemEventLogs,
    Geolocation,
    ThreatProtectionApplications
}