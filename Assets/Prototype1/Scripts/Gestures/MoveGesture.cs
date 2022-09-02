using Zinnia.Action;
using UnityEngine;

public class MoveGesture : Gesture
{
    // Avatar
    [SerializeField] Transform Avatar;
    [SerializeField] Transform Headset;

    // Left Hand
    [SerializeField] BooleanAction LeftTriggerPress;
    [SerializeField] Transform LeftController;

    // Right Hand
    [SerializeField] BooleanAction RightTriggerPress;
    [SerializeField] Transform RightController;

    enum ControllerLR { Left, Right }
    private ControllerLR TriggeredController;
    private Vector3 InitialAvatarPosition;
    private Vector3 InitialControllerPosition;

    // Gesture Overrides
    public override bool Trigger() => LeftTriggerPress.Value ^ RightTriggerPress.Value;
    public override void StartGesture()
    {
        InitialAvatarPosition = Avatar.position;
        
        TriggeredController = LeftTriggerPress.Value ? ControllerLR.Left : ControllerLR.Right;
        InitialControllerPosition = GetRelativeControllerPosition();
    }
    public override void GestureActive() => Avatar.position = CalculatePosition();

    // Movement Calculations
    private Vector3 GetRelativeControllerPosition() =>
        TriggeredController switch
        {
            ControllerLR.Left => Headset.InverseTransformPoint(LeftController.position),
            ControllerLR.Right => Headset.InverseTransformPoint(RightController.position),
            _ => throw new System.Exception("Invalid TriggeredController")
        };
    private Vector3 CalculatePosition()
    {
        float movementMultiplier = 4;
        Vector3 relativeMovement = (GetRelativeControllerPosition() - InitialControllerPosition) * movementMultiplier;
        relativeMovement.y = relativeMovement.y * -1;
        Vector3 newPosition = InitialAvatarPosition + relativeMovement;
        return newPosition;
    }
}