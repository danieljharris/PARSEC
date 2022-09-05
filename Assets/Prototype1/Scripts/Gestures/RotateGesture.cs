using Zinnia.Action;
using UnityEngine;

public class RotateGesture : Gesture
{
    // Avatar
    [SerializeField] Transform Avatar;

    // Controller Transforms
    [SerializeField] Transform LeftController;
    [SerializeField] Transform RightController;

    // Trigger Actions
    [SerializeField] BooleanAction LeftTriggerPress;
    [SerializeField] BooleanAction RightTriggerPress;

    private Quaternion InitialControllerRotation;
    private Quaternion InitialRotation;

    public RotateGesture(Transform Avatar,
                         Transform Aliases,
                         BooleanAction LeftTriggerPress,
                         BooleanAction RightTriggerPress)
    {
        this.Avatar = Avatar;

        this.LeftController  = Aliases.Find("LeftControllerAlias");
        this.RightController = Aliases.Find("RightControllerAlias");

        this.LeftTriggerPress = LeftTriggerPress;
        this.RightTriggerPress = RightTriggerPress;
    }

    // Gesture Overrides
    public override bool Trigger() => LeftTriggerPress.Value && RightTriggerPress.Value;
    public override void StartGesture()
    {
        InitialControllerRotation = ControllerRotation();
        InitialRotation = Avatar.rotation;
    }
    public override void GestureActive() => Avatar.rotation = CalculateRotation();

    // Rotation Calculations
    private Quaternion ControllerRotation() =>
        Quaternion.LookRotation(LeftController.position - RightController.position);
    private Quaternion CalculateRotation()
    {
        Quaternion rotationDifference = InitialControllerRotation * Quaternion.Inverse(ControllerRotation());
        // Quaternion newRotation = InitialRotation + Vector3.one * rotationFactor;
        Quaternion newRotation = InitialRotation * rotationDifference;
        return newRotation;
    }
}