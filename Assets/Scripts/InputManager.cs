using HuangQiaoxin.Input;
using HuangQiaoxin.Lab2;
using HuangQiaoxin.Lab3;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Define variables including assigning the specified maze and the camera parent
    private CSE3541Inputs inputScheme;
    [SerializeField] private RotateControl maze;
    [SerializeField] private CameraSwitcher cameraSwitcher;

    // Define variables related to the movement, the player, the camera
    [SerializeField] private MovementControl movementController;
    [SerializeField] private CameraRotate cameraRotate;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private FollowWithOffset followWithOffset;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, 0f);

    // Initialization
    private void Awake()
    {
        inputScheme = new CSE3541Inputs();
        maze.Initialize(inputScheme.Player.Rotate);

        // Initialize the camera rotation and the movement, and the offset between the camera and the player
        cameraRotate.Initialize(inputScheme.Player.CameraRotate);
        movementController.Initialize(inputScheme.Player.Move, inputScheme.Player.ChangeSpeed);
        followWithOffset = new FollowWithOffset(cameraTransform, playerTransform, offset);
    }

    // Enables quit handler and switch camera handler
    private void OnEnable()
    {
        inputScheme.Player.Enable();
        var _ = new QuitHandler(inputScheme.Player.Quit);
        var nextCameraHandler = new NextCameraHandler(inputScheme.Player.CameraSwitch, this.cameraSwitcher);
    }

    // Update the movement and the offset for the camera
    private void Update()
    {
        movementController.Update();
        followWithOffset.UpdatePosition();
    }
}