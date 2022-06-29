using System.Linq;
using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    public void ResetScene()
    {
        FindObjectsOfType<Resettable>().ToList().ForEach(r => r.ResetTransform());
    }
    public void ResetPlayer()
    {
        FindObjectsOfType<ResettableList>().ToList().ForEach(r => r.ResetTransform());
    }
}