using UnityEngine;

public abstract class Gesture : MonoBehaviour
{
    private bool Active = false;

    void Update()
    {
        // Stop Gesture
        if (Active && !Trigger())
        {
            Active = false;
            StopGesture();
            return;
        }

        // Start Gesture
        if(!Active && Trigger())
        {
            Active = true;
            StartGesture();
        }

        // Gesture in progress
        if (Active)
        {
            GestureActive();
        }
    }

    abstract public bool Trigger();
    virtual public void StartGesture(){}
    virtual public void StopGesture(){}
    virtual public void GestureActive(){}
}