using Zinnia.Action;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    [SerializeField] Transform Avatar, Aliases; // Avatar + Aliases
    [SerializeField] BooleanAction LButton,  RButton; // Button Actions
    [SerializeField] GameObject debugCubePrefab; // Debugging tool

    private Gesture[] Gestures;

    void Start()
    {
        GameObject debugCube = Instantiate(debugCubePrefab);

        Gestures = new Gesture[]
        {
            // new MoveGesture(Avatar, Aliases, LButton, RButton),
            // new ScaleGesture(Avatar, Aliases, LButton, RButton),
            new RotateGesture(Avatar, Aliases, LButton, RButton, debugCube)
        };
    }

    void Update()
    {
        foreach (Gesture gesture in Gestures) gesture.Run();
    }
}