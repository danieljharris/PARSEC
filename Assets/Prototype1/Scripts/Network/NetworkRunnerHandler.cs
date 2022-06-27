using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private string _SessionName;

    void Start()
    {
        NetworkRunner runner = GetComponent<NetworkRunner>();
        runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = _SessionName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager  = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}