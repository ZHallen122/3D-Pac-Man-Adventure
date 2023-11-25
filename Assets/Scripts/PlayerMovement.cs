using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 2f;
    private Vector2 moveAction;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveAction.x, 0, moveAction.y);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += receiveMove;
        inputActions.Player.Move.canceled += receiveMove;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= receiveMove;
        inputActions.Player.Move.canceled -= receiveMove;
        inputActions.Player.Disable();
    }

    public void receiveMove(InputAction.CallbackContext obj)
    {
        moveAction = obj.ReadValue<Vector2>();
    }
}