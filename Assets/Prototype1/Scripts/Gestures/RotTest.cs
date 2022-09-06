using Zinnia.Action;
using UnityEngine;

public class RotTest : MonoBehaviour
{
    [SerializeField] Transform Avatar;
    [SerializeField] Transform LeftControllerAlias, RightControllerAlias; // Avatar + Controllers
    [SerializeField] GameObject LeftInteractor, RightInteractor; // Avatar + Controllers
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions

    [SerializeField] GameObject debugCubePrefab; // Debugging tool
    [SerializeField] GameObject debugCubePrefab2; // Debugging tool
    private GameObject debugCube; //Debugging tool
    private GameObject debugCube2; //Debugging tool


    private bool LControllerActive;
    private bool Trigger() => LButton.Value ^ RButton.Value;
    private bool Active = false;


    void Update()
    {
        // Gesture in progress
        if (Active)
        {
            // debugCube2.transform.rotation = Quaternion.Inverse(debugCube.transform.rotation);
            // Avatar.transform.rotation = Quaternion.Inverse(debugCube.transform.rotation);

            // debugCube2.transform.rotation = Quaternion.Inverse(debugCube.transform.localRotation);
            Avatar.transform.rotation = Quaternion.Inverse(debugCube.transform.localRotation);
        }

        // Stop Gesture
        if (Active && !Trigger())
        {
            Debug.Log("Stop Gesture");

            Active = false;

            // Destroy(debugCube);

            FixedJoint joint = debugCube.GetComponent(typeof(FixedJoint)) as FixedJoint;
            joint.connectedBody = null;

            return;
        }

        // Start Gesture
        if(!Active && Trigger())
        {
            Debug.Log("Start Gesture");

            Active = true;
            LControllerActive = LButton.Value;

            if (debugCube == null)
            {
                Vector3 pos = LControllerActive ? LeftControllerAlias.position : RightControllerAlias.position;
                debugCube = Instantiate(
                    debugCubePrefab,
                    pos,
                    Quaternion.identity,
                    Avatar);

                Vector3 posOffset = pos;
                posOffset.x -= 0.1f;
                
                debugCube2 = Instantiate(
                    debugCubePrefab2,
                    posOffset,
                    Quaternion.identity);
            }

            FixedJoint joint = debugCube.GetComponent(typeof(FixedJoint)) as FixedJoint;

            if (joint == null) Debug.Log("Error: No joint");

            if (LControllerActive) joint.connectedBody = LeftInteractor.GetComponent<Rigidbody>();
            else joint.connectedBody = RightInteractor.GetComponent<Rigidbody>();
        }
    }
}