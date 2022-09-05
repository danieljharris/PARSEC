using Zinnia.Action;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    // Avatar
    [SerializeField] Transform Avatar;
    [SerializeField] Transform Aliases;
    [SerializeField] BooleanAction LeftTriggerPress;
    [SerializeField] BooleanAction RightTriggerPress;

    Gesture[] Gestures;

    void Start()
    {
        Gestures = new Gesture[]
        {
            // new MoveGesture(Avatar, Aliases, LeftTriggerPress, RightTriggerPress),
            // new ScaleGesture(Avatar, Aliases, LeftTriggerPress, RightTriggerPress),
            new RotateGesture(Avatar, Aliases, LeftTriggerPress, RightTriggerPress)
        };
    }

    void Update()
    {
        foreach (Gesture gesture in Gestures) gesture.Run();
    }
}