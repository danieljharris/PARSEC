using Zinnia.Action;
using UnityEngine;

public class ScaleGesture : Gesture
{
    // Avatar
    [SerializeField] Transform Avatar;

    // Left Hand
    [SerializeField] BooleanAction LeftTriggerPress;
    [SerializeField] Transform LeftController;

    // Right Hand
    [SerializeField] BooleanAction RightTriggerPress;
    [SerializeField] Transform RightController;

    private float InitialDistance;
    private Vector3 InitialScale;

    // Gesture Overrides
    public override bool Trigger() => LeftTriggerPress.Value && RightTriggerPress.Value;
    public override void StartGesture()
    {
        InitialDistance = ControllerSpreadDistance();
        InitialScale = Avatar.localScale;
    }
    public override void GestureActive() => Avatar.localScale = CalculateScale();

    // Scale Calculations
    private float ControllerSpreadDistance() =>
        Vector3.Distance(LeftController.position, RightController.position);
    private Vector3 CalculateScale()
    {
        float scaleFactor = InitialDistance - ControllerSpreadDistance();
        Vector3 newScale = InitialScale + Vector3.one * scaleFactor;
        return newScale;
    }
}