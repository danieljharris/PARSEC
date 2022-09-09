using Zinnia.Action;
using UnityEngine;

public class RotTest : MonoBehaviour
{
    [SerializeField] Transform Avatar;
    [SerializeField] GameObject RightControllerTip;
    [SerializeField] Transform RightControllerAlias; // Avatar + Controllers
    [SerializeField] GameObject RightInteractor; // Avatar + Controllers
    [SerializeField] BooleanAction RButton; // Button Actions

    [SerializeField] GameObject debugCubePrefab; // Debugging tool
    [SerializeField] GameObject debugCubePrefab2; // Debugging tool
    private GameObject debugCube; //Debugging tool
    private GameObject debugCube2; //Debugging tool


    private bool Trigger() => RButton.Value;
    private bool Active = false;



    private Quaternion InitControllerRot, InitDebugRot;


    void Update()
    {
        // Stop Gesture
        if (Active && !Trigger())
        {
            Debug.Log("Stop Gesture");

            Active = false;
            RightControllerTip.GetComponent<Renderer>().material.color = Color.red;


            // Destroy(debugCube);

            FixedJoint joint = debugCube.GetComponent(typeof(FixedJoint)) as FixedJoint;
            joint.connectedBody = null;

            return;
        }

        // Gesture in progress
        if (Active && Trigger())
        {
            // debugCube2.transform.rotation = Quaternion.Inverse(debugCube.transform.rotation);
            // Avatar.transform.rotation = Quaternion.Inverse(debugCube.transform.rotation);

            // debugCube2.transform.rotation = Quaternion.Inverse(debugCube.transform.localRotation); // Working example 1
            // Avatar.transform.rotation = Quaternion.Inverse(debugCube.transform.localRotation); // Working example 2

            // debugCube.transform.localRotation = RightInteractor.transform.rotation * Quaternion.Inverse(InitControllerRot) * InitDebugRot; // AI
            // debugCube2.transform.localRotation = Quaternion.Inverse(debugCube.transform.localRotation);


            // Quaternion NewRot = RightInteractor.transform.rotation * Quaternion.Inverse(InitControllerRot) * InitDebugRot;

            // NewRot.y = -NewRot.y;

            // debugCube.transform.localRotation = NewRot;
            // debugCube2.transform.localRotation = Quaternion.Inverse(debugCube.transform.localRotation);


            // Nope - Only Barrel Correct
            // debugCube.transform.localRotation = (RightInteractor.transform.rotation * Quaternion.Inverse(InitControllerRot)) * InitDebugRot;

            // Nope - Only tilt forward and back
            // debugCube.transform.localRotation = Quaternion.Inverse(InitControllerRot) * RightInteractor.transform.rotation * InitDebugRot;

            // Nope - All wrong (w/ inverted tilt forward and back)
            // debugCube.transform.localRotation = Quaternion.Inverse(RightInteractor.transform.rotation) * InitControllerRot * InitDebugRot;

            // Nope - Inverted Barrel
            // debugCube.transform.localRotation = InitControllerRot * Quaternion.Inverse(RightInteractor.transform.rotation) * InitDebugRot;

            // Nope
            // debugCube.transform.localRotation = InitControllerRot * RightInteractor.transform.rotation * InitDebugRot;
            // debugCube.transform.localRotation = InitControllerRot * RightInteractor.transform.rotation * Quaternion.Inverse(InitDebugRot);


            // // Nope - Only Barrel Correct
            // Quaternion NewRot1 = RightInteractor.transform.rotation * Quaternion.Inverse(InitControllerRot) * InitDebugRot;

            // // Nope - Inverted Barrel
            // Quaternion NewRot2 = InitControllerRot * Quaternion.Inverse(RightInteractor.transform.rotation) * InitDebugRot;

            // Quaternion NewRot3 = NewRot2;
            // NewRot3.w = NewRot1.w;

            // debugCube.transform.localRotation = NewRot3;
        }

        // Start Gesture
        if(!Active && Trigger())
        {
            Debug.Log("Start Gesture");

            Active = true;
            RightControllerTip.GetComponent<Renderer>().material.color = Color.green;

            if (debugCube == null)
            {
                Vector3 pos = RightControllerAlias.position;

                debugCube = Instantiate(
                    debugCubePrefab,         // Swap if needed
                    // debugCubePrefab2,         // Swap if needed
                    pos,
                    Quaternion.identity,
                    Avatar);

                // Vector3 posOffset = pos;
                // posOffset.x -= 0.1f;
                
                // debugCube2 = Instantiate(
                //     debugCubePrefab2,
                //     posOffset,
                //     Quaternion.identity);
            }

            // InitControllerRot = RightInteractor.transform.rotation;
            // InitDebugRot = debugCube.transform.localRotation;

            FixedJoint joint = debugCube.GetComponent(typeof(FixedJoint)) as FixedJoint;
            if (joint == null) Debug.Log("Error: No joint");
            joint.connectedBody = RightInteractor.GetComponent<Rigidbody>();
        }
    }
}