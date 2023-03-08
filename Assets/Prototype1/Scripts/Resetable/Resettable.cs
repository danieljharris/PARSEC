using Fusion;
using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
    public void ResetTransform()
    {
        NetworkObject netObj = GetComponent<NetworkObject>();
        if(netObj != null)
        {
            netObj.RequestStateAuthority();
        }

        // Allow time for state authority to be granted
        Invoke(nameof(Reset), 0.1f);
        netObj.ReleaseStateAuthority();
    }
    private void Reset()
    {
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
    }
}