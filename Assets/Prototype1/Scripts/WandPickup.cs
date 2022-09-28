using UnityEngine;

public class WandPickup : MonoBehaviour
{
    public void OnGrab()
    {
        this.transform.parent = null;
        this.transform.localScale = Vector3.one;
    }
}