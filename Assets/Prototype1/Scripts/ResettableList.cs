using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ResettableList : MonoBehaviour
{
    public List<GameObject> ResettableObjects = new List<GameObject>();
    private List<(GameObject, Vector3, Quaternion)> _resettableObjectsOrigins = new List<(GameObject, Vector3, Quaternion)>();
    void Start()
    {
        foreach(GameObject go in ResettableObjects)
            _resettableObjectsOrigins.Add((go, go.transform.position, go.transform.rotation));
    }
    public void ResetTransform()
    {
        Debug.Log("ResetTransform");

        NetworkObject netObj = GetComponent<NetworkObject>();
        // if(netObj == null || !netObj.HasInputAuthority || !netObj.HasStateAuthority) 
        // {
        //     return;
        // }

        Debug.Log("ResetTransform count: " + _resettableObjectsOrigins.Count);

        foreach((GameObject go, Vector3 pos, Quaternion rot) in _resettableObjectsOrigins)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
        }
    }
}