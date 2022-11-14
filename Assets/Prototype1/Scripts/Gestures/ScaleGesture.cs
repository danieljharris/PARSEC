using Zinnia.Action;
using UnityEngine;
using static Utility;

public class ScaleGesture : Gesture
{
    [SerializeField] Transform Avatar, Headset, LController, RController; // Avatar + Headset + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    private float InitialDistance;
    private Vector3 InitialScale;
    private float previousDistance = 0f;


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

        // Set min bounds for scaling
        if (newScale.x < 0.1f) return;

        Vector3 newPosition = CalculatePosition(newScale);

        Avatar.localScale = newScale;
        Avatar.localPosition = newPosition;
    }


    // Scale Calculations
    private float ControllerSpreadDistance() =>
        Vector3.Distance(LController.localPosition, RController.localPosition);
    private Vector3 CalculateScale()
    {
        /* 
        New Scale = Previous Scale of User
                    + ((Starting Distance between Controllers
                    - Current Distance Between Controllers) * Scale Multiplier)
        */

        float scaleMultiplier = Avatar.localScale.x;

        float currentDistance = ControllerSpreadDistance();
        float distanceMoved = InitialDistance - currentDistance;
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
}