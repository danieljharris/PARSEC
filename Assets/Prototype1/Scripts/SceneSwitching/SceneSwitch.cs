using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        // Get the NetworkRunner and shut it down
        NetworkRunner runner = GameObject.FindGameObjectWithTag("FusionNetworkRunner").GetComponent<NetworkRunner>();
        runner.Shutdown();

        // Load the next scene which creates a new NetworkRunner
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}