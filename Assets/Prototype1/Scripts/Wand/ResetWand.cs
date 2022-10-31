using UnityEngine;

public class ResetWand : MonoBehaviour
{
    [SerializeField] ResettableWand Wand;
    public void Reset() => Wand.Reset();
}