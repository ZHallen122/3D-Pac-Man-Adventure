using ShareefSoftware;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HuangQiaoxin.Lab3
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private LevelGeneration levelGeneration;

        private InputAction moveAction;
        private InputAction shiftAction;    // 'shift' key for speeding up

        private float regularSpeed = 2f;
        private float largerSpeed = 5f;
        private float currentSpeed;
        private bool isMoving = false;
        Vector3 movement;

        private void Start()
        {
            // Set the player's position to the starting cell's center position.
            transform.position = levelGeneration.GetStartingCellCenterPosition();

            // Initialize the current speed.
            currentSpeed = regularSpeed;
        }

        public void Initialize(InputAction moveAction, InputAction shiftAction)
        {
            // Move action
            this.moveAction = moveAction;
            moveAction.performed += MoveAction_performed;
            moveAction.canceled += MoveAction_canceled;
            moveAction.Enable();

            // Shift action
            this.shiftAction = shiftAction;
            shiftAction.performed += ShiftAction_performed;
            shiftAction.canceled += ShiftAction_canceled;   // Used for releasing the 'shift' key
            shiftAction.Enable();
        }

        private void MoveAction_performed(InputAction.CallbackContext obj)
        {
            Vector2 moveInput = obj.ReadValue<Vector2>();
            isMoving = true;

            // Get a new movement
            movement = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
        }

        private void MoveAction_canceled(InputAction.CallbackContext obj)
        {
            isMoving = false;
        }

        public void Update()
        {
            // Move the player from the originial position to a new position
            if (isMoving)
            {
                var step = currentSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, step);
            }
        }

        private void ShiftAction_performed(InputAction.CallbackContext obj)
        {
            currentSpeed = largerSpeed; // Increase the current movement speed when the 'shift' key is pressed
        }

        private void ShiftAction_canceled(InputAction.CallbackContext obj)
        {
            currentSpeed = regularSpeed; // Reset the speed to the regular speed when the 'shift' key is released
        }
    }
}
