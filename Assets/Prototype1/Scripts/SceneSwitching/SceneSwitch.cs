using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
}