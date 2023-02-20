using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;

using static Detectability;

// C# 9.0 workaround for Unity .NET 5
namespace System.Runtime.CompilerServices { internal static class IsExternalInit {}}

public class NodeSpecs : MonoBehaviour
{
    private static int NumberOfAttackTypes = 11;

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

    NodeSpecs()
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

    public Detectability GetAttackTypeDetectability(AttackType attackType)
    {
        return AttackTypeDetectability[(int)attackType];
    }

    public bool ContainsWare(List<Software> software, List<Hardware> hardware)
    {
        return software.All(this.software.Contains) &&
               hardware.All(this.hardware.Contains); 
    }

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