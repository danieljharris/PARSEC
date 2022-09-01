using Zinnia.Action;
using UnityEngine;
// using static Db;

public class ScalePerspectiveGesture : MonoBehaviour
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

    private bool Active = false;
    private float InitialDistance;
    private Vector3 InitialScale;
    private Vector3 InitialPosition;
    private Vector3 InitialRelativeMidPoint;

    // Update is called once per frame
    void Update()
    {
        // Stop Gesture
        if (Active && (!LeftTriggerPress.Value || !RightTriggerPress.Value))
        {
            Active = false;
            return;
        }

        // Start Gesture
        if(!Active && LeftTriggerPress.Value && RightTriggerPress.Value)
        {
            Active = true;
            InitialDistance = ControllerSpreadDistance();
            InitialRelativeMidPoint = GetRelativeMidPoint();
            InitialScale = Avatar.localScale;
            InitialPosition = Avatar.position;
        }

        // Gesture in progress
        if (Active)
        {
            Avatar.localScale = CalculateScale();
            Avatar.position = CalculatePosition();
        }
    }

    private float ControllerSpreadDistance() =>
        Vector3.Distance(LeftController.position, RightController.position);
    private Vector3 CalculateScale()
    {
        float scaleFactor = InitialDistance - ControllerSpreadDistance();
        Vector3 newScale = InitialScale + Vector3.one * scaleFactor;
        return newScale;
    }

    private Vector3 GetRelativeMidPoint()
    {
        Vector3 midPoint = new Vector3();
        midPoint.x = RightController.position.x + (LeftController.position.x - RightController.position.x) / 2;
        midPoint.y = RightController.position.y + (LeftController.position.y - RightController.position.y) / 2;
        midPoint.z = RightController.position.z + (LeftController.position.z - RightController.position.z) / 2;

        Vector3 relativeMidPoint = Headset.InverseTransformPoint(midPoint);
        return relativeMidPoint;
    }
    private Vector3 CalculatePosition()
    {
        float movementMultiplier = 4;
        Vector3 relativeMovement = (GetRelativeMidPoint() - InitialRelativeMidPoint) * movementMultiplier;
        relativeMovement.y = relativeMovement.y * -1;
        Vector3 newPosition = InitialPosition + relativeMovement;
        return newPosition;
    }
}
