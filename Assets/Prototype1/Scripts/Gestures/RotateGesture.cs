using Zinnia.Action;
using UnityEngine;

public class RotateGesture : SingleHandGesture
{
    [SerializeField] Transform Avatar, LController, RController; // Avatar + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    private Transform ActiveController;
    private Vector3 InitControllerForward;
    private Quaternion InitControllerRot, InitRelativeControllerRot, InitAvatarRot, InitDebugRot;
    private GameObject debugCube; //Debugging tool





    public RotateGesture(Transform Avatar,
                         Transform Aliases,
                         BooleanAction LButton,
                         BooleanAction RButton,
                         GameObject debugCube)
    {
        this.Avatar = Avatar;

        this.LController = Aliases.Find("LeftControllerAlias");
        this.RController = Aliases.Find("RightControllerAlias");

        this.LButton = LButton;
        this.RButton = RButton;

        this.debugCube = debugCube;
    }

    // Gesture Overrides
    public override bool LeftButton() => LButton.Value;
    public override bool RightButton() => RButton.Value;
    public override void StartGesture()
    {
        base.StartGesture();
        ActiveController = LControllerActive ? LController : RController;

        InitControllerForward = ActiveController.forward;
        InitControllerRot = ControllerRotation();
        // InitRelativeControllerRot = ControllerRelativeRotation();
        InitAvatarRot = Avatar.rotation;

        InitDebugRot = debugCube.transform.rotation;
    }
    // public override void GestureActive() => Avatar.rotation = CalculateRotation();
    public override void GestureActive()
    {
        // Avatar.rotation = CalculateRotation();

        // debugCube.transform.rotation = CalculateRotation();
        // debugCube.transform.rotation = CalculateRotation();
        // Avatar.rotation = ControllerRotation();

        // debugCube.transform.rotation = CalculateDebugRotation();
        // debugCube.transform.Rotate(CalculateDebugRotation().eulerAngles);

        // Avatar.transform.Rotate(CalculateRotation().eulerAngles, Space.Self);

        // debugCube.transform.rotation = ActiveController.rotation;
        // debugCube.transform.rotation = ActiveController.localRotation;
        // debugCube.transform.rotation = InitDebugRot * ActiveController.localRotation;

        // debugCube.transform.rotation = InitDebugRot * (ActiveController.localRotation * Quaternion.Inverse(InitControllerRot));
        // debugCube.transform.rotation =
        //     InitDebugRot *
        //     Quaternion.FromToRotation(
        //         InitControllerRot.eulerAngles,
        //         ControllerRotation().eulerAngles);

        // debugCube.transform.rotation =
        //     InitDebugRot *
        //     Quaternion.FromToRotation(
        //         InitControllerForward,
        //         ActiveController.forward);

        // debugCube.transform.rotation =
        //     InitDebugRot *
        //     Avatar.InverseTransformDirection(ActiveController.forward);
    }

    // Rotation Calculations
    // private Quaternion ControllerRelativeRotation() =>
    //     // Quaternion.FromToRotation(Avatar.transform.forward, ActiveController.forward);
    //     Quaternion.FromToRotation(Avatar.transform.up, ActiveController.up);
    private Quaternion ControllerRotation() => ActiveController.localRotation;
    // private Quaternion CalculateRotation()
    // {
    //     Quaternion relativeRotation = Quaternion.Inverse(ControllerRotation()) * InitRelativeControllerRot;
    //     Quaternion newRotation = InitAvatarRot * relativeRotation;
    //     // Quaternion newRotation = InitAvatarRot * Quaternion.Inverse(relativeRotation); // Dream Idea
    //     // Quaternion newRotation = Quaternion.Inverse(relativeRotation) * InitAvatarRot; // Dream Idea
    //     // Quaternion newRotation = relativeRotation * InitAvatarRot;
    //     return newRotation;
    // }

    // private Quaternion CalculateDebugRotation()
    // {
    //     Quaternion relativeRotation = Quaternion.Inverse(ControllerRelativeRotation()) * InitRelativeControllerRot;
    //     // Quaternion newRotation = relativeRotation * InitDebugRot;
    //     Quaternion newRotation = InitDebugRot * relativeRotation;
    //     // Quaternion newRotation = Quaternion.Inverse(relativeRotation) * InitDebugRot; // Dream Idea

    //     Debug.Log("-----------------------------------------");
    //     Debug.Log("InitDebugRot: " + InitDebugRot);
    //     Debug.Log("InitRelativeControllerRot: " + InitRelativeControllerRot);
    //     Debug.Log("ControllerRelativeRotation(): " + ControllerRelativeRotation());
    //     Debug.Log("relativeRotation: " + relativeRotation);
    //     Debug.Log("newRotation: " + newRotation);

    //     return newRotation;
    // }
}