using UnityEngine;
using UnityEngine.Events;
public class Menu : MonoBehaviour
{
    public UnityEvent Setup = new UnityEvent();
    public UnityEvent Show = new UnityEvent();
    public UnityEvent Hide = new UnityEvent();
    void Start() => Setup.Invoke();
}