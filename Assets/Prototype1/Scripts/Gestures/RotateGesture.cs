using Zinnia.Action;
using UnityEngine;

public class RotateGesture : SingleHandGesture
{
    [SerializeField] Transform Avatar; // Avatar
    [SerializeField] GameObject LInteractor, RInteractor; // Controller Interactors
    [SerializeField] GameObject RotationCubePrefab;
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions


    // Used to store the rotation of the player's hand in relation to the avatar
    private GameObject RotationCube;


    // Gesture Overrides
    public override bool LeftButton() => LButton.Value;
    public override bool RightButton() => RButton.Value;
    public override void StartGesture()
    {
        base.StartGesture();
        
        if (RotationCube == null)
        {
            RotationCube = Instantiate(
                RotationCubePrefab,
                Vector3.zero,
                Quaternion.identity,
                Avatar);
        }

        FixedJoint joint = RotationCube.GetComponent(typeof(FixedJoint)) as FixedJoint;
        GameObject ActiveInteractor = LControllerActive ? LInteractor : RInteractor;
        joint.connectedBody = ActiveInteractor.GetComponent<Rigidbody>();
    }
    public override void GestureActive() =>
        Avatar.transform.rotation = Quaternion.Inverse(RotationCube.transform.localRotation);
    public override void StopGesture()
    {
        FixedJoint joint = RotationCube.GetComponent(typeof(FixedJoint)) as FixedJoint;
        joint.connectedBody = null;
    }
}