using Zinnia.Action;
using UnityEngine;

public class MoveGesture : SingleHandGesture
{
    [SerializeField] Transform Avatar, LController, RController; // Avatar + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    private Vector3 InitialAvatarPosition, InitialControllerPosition;


    // Gesture Overrides
    public override bool LeftButton() => LButton.Value;
    public override bool RightButton() => RButton.Value;
    public override void StartGesture()
    {
        base.StartGesture();
        InitialAvatarPosition = Avatar.position;
        InitialControllerPosition = GetRelativeControllerPosition();
    }
    public override void ApplyGesture() => Avatar.position = CalculatePosition();


    // Movement Calculations
    private Vector3 GetRelativeControllerPosition()
    {
        if (LControllerActive)
            return Avatar.InverseTransformPoint(LController.position);
        else
            return Avatar.InverseTransformPoint(RController.position);
    }
    private Vector3 CalculatePosition()
    {
        float movementMultiplier = 3 + Avatar.localScale.x;
        Vector3 controllerMovement = GetRelativeControllerPosition() - InitialControllerPosition;
        Vector3 relativeMovement = controllerMovement * movementMultiplier;
        relativeMovement.y = relativeMovement.y * -1; // fix relative movement of y axis
        Vector3 newPosition = InitialAvatarPosition + relativeMovement;
        return newPosition;
    }
}