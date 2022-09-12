using Zinnia.Action;
using UnityEngine;

public class MoveGesture : SingleHandGesture
{
    [SerializeField] Transform Avatar, Headset, LController, RController; // Avatar + Headset + Controllers
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
    public override void GestureActive() => Avatar.position = CalculatePosition();


    // Movement Calculations
    private Vector3 GetRelativeControllerPosition()
    {
        if (LControllerActive) return Headset.InverseTransformPoint(LController.position);
        else                   return Headset.InverseTransformPoint(RController.position);
    }
    private Vector3 CalculatePosition()
    {
        float movementMultiplier = 4;
        Vector3 relativeMovement = (GetRelativeControllerPosition() - InitialControllerPosition) * movementMultiplier;
        relativeMovement.y = relativeMovement.y * -1;
        Vector3 newPosition = InitialAvatarPosition + relativeMovement;
        return newPosition;
    }
}