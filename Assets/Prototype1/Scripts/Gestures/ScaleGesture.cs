using Zinnia.Action;
using UnityEngine;

public class ScaleGesture : Gesture
{
    // Avatar
    [SerializeField] Transform Avatar;

    // Controller Transforms
    [SerializeField] Transform LeftController;
    [SerializeField] Transform RightController;

    // Trigger Actions
    [SerializeField] BooleanAction LeftTriggerPress;
    [SerializeField] BooleanAction RightTriggerPress;

    private float InitialDistance;
    private Vector3 InitialScale;

    public ScaleGesture(Transform Avatar,
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