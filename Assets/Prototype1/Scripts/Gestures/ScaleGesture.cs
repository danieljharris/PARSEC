using Zinnia.Action;
using UnityEngine;

public class ScaleGesture : Gesture
{
    [SerializeField] Transform Avatar, LController, RController; // Avatar + Headset + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    private float InitialDistance;
    private Vector3 InitialScale;

    public ScaleGesture(Transform Avatar,
                        Transform Aliases,
                        BooleanAction LButton,
                        BooleanAction RButton)
    {
        this.Avatar = Avatar;

        this.LController = Aliases.Find("LeftControllerAlias");
        this.RController = Aliases.Find("RightControllerAlias");

        this.LButton = LButton;
        this.RButton = RButton;
    }

    // Gesture Overrides
    public override bool Trigger() => LButton.Value && RButton.Value;
    public override void StartGesture()
    {
        InitialDistance = ControllerSpreadDistance();
        InitialScale = Avatar.localScale;
    }
    public override void GestureActive() => Avatar.localScale = CalculateScale();

    // Scale Calculations
    private float ControllerSpreadDistance() =>
        Vector3.Distance(LController.position, RController.position);
    private Vector3 CalculateScale()
    {
        float scaleFactor = InitialDistance - ControllerSpreadDistance();
        Vector3 newScale = InitialScale + Vector3.one * scaleFactor;
        return newScale;
    }
}