// Author: Qiaoxin Huang, Allen Zhang

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4f;
    private Vector2 moveAction;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveAction.x, 0, moveAction.y);
        if (movement != Vector3.zero)
        {
            // rotation to face the direction of movement
            transform.rotation = Quaternion.LookRotation(movement);
        }

        // Move Pac-Man
        transform.Translate(movement.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += ReceiveMove;
        inputActions.Player.Move.canceled += ReceiveMove;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ReceiveMove;
        inputActions.Player.Move.canceled -= ReceiveMove;
        inputActions.Player.Disable();
    }

    public void ReceiveMove(InputAction.CallbackContext context)
    {
        moveAction = context.ReadValue<Vector2>();
    }
}
