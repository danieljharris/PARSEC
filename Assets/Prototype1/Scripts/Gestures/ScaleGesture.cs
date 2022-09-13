using Zinnia.Action;
using UnityEngine;

public class ScaleGesture : Gesture
{
    [SerializeField] Transform Avatar, Headset, LController, RController; // Avatar + Headset + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    private float InitialDistance;
    private Vector3 InitialScale;


    // Gesture Overrides
    public override bool Trigger() => LButton.Value && RButton.Value;
    public override void StartGesture()
    {
        InitialDistance = ControllerSpreadDistance();
        InitialScale = Avatar.localScale;
    }
    public override void ApplyGesture()
    {
        Vector3 newScale = CalculateScale();
        Vector3 newPosition = CalculatePosition(newScale);

        Avatar.localScale = newScale;
        Avatar.localPosition = newPosition;
    }


    // Scale Calculations
    private float ControllerSpreadDistance() =>
        Vector3.Distance(LController.position, RController.position);
    private Vector3 CalculateScale()
    {
        float scaleMultiplier = 2;

        float distanceMoved = InitialDistance - ControllerSpreadDistance();
        float scaleFactor = distanceMoved * scaleMultiplier;

        Vector3 scaleFactorVector = Vector3.one * scaleFactor;
        Vector3 newScale = InitialScale + scaleFactorVector;

        return newScale;
    }


    // Position Calculations
    // To set pivot point just below headset
    private Vector3 CalculatePosition(Vector3 newScale)
    {
        Vector3 pivot = Headset.position;
        pivot.y = pivot.y - 2f;

        Vector3 newPosition = ScaleAround(Avatar, pivot, newScale.x);

        return newPosition;
    }
    public Vector3 ScaleAround(Transform target, Vector3 pivot, float newScaleX)
    {
        Vector3 difference = target.localPosition - pivot;
        float relativeScale = newScaleX / target.localScale.x;
        Vector3 newPosition = pivot + difference * relativeScale;
        return newPosition;
    }
}