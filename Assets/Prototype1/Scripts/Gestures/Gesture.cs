using UnityEngine;

public abstract class Gesture
{
    private bool Active = false;

    public void Run()
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