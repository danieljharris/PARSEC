using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]
    public void RPC_LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
}