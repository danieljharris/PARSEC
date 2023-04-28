using TMPro;
using UnityEngine;

public class InfoReader : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    public GameObject infoPanel;
    public TextMeshPro infoPanelText;
    private GameObject colliderObject;

    public void IsEnabled(bool value) => isEnabled = value;

    void OnTriggerEnter(Collider other)
    {
        if(!isEnabled) return;

        ObjectInfo objectInfo = other?.gameObject?.GetComponent<ObjectInfo>();
        if (objectInfo == null) return;

        infoPanelText.text = objectInfo.info;
        infoPanel.SetActive(true);

        colliderObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if(!isEnabled) return;

        ObjectInfo objectInfo = other?.gameObject?.GetComponent<ObjectInfo>();
        if (objectInfo == null) return;

        // Allows InfoReader to move from one object into another object that is close/inside
        if (objectInfo.gameObject != colliderObject) return;

        infoPanel.SetActive(false);
    }
}