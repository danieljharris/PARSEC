using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private string _SessionName;

    void Awake()
    {
        int numberOfRunners = Object.FindObjectsOfType<NetworkRunnerHandler>().Length;
        if(numberOfRunners > 1) Destroy(gameObject);
    }

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