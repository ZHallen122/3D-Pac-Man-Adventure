using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4f;
    private Vector2 moveAction;
    private InputActions inputActions;
    private float originalSpeed;

    public GameObject lightBallPrefab;
    public Transform shootPoint;
    public float shootForce = 1000f;

    private bool canShoot = false;

    private void Awake()
    {
        inputActions = new InputActions();
        originalSpeed = speed;
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
        inputActions.Player.Shoot.performed += _ => ShootLightBall();
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ReceiveMove;
        inputActions.Player.Move.canceled -= ReceiveMove;
        inputActions.Player.Shoot.performed -= _ => ShootLightBall();
        inputActions.Player.Disable();
    }

    public void ReceiveMove(InputAction.CallbackContext context)
    {
        moveAction = context.ReadValue<Vector2>();
    }

    IEnumerator EnableShootingForDuration(float duration)
    {
        canShoot = true;
        yield return new WaitForSeconds(duration);
        canShoot = false;
    }

    public void ShootLightBall()
    {
        if (canShoot)
        {
            GameObject lightBall = Instantiate(lightBallPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = lightBall.GetComponent<Rigidbody>();
            rb.AddForce(shootPoint.forward * shootForce);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SuperSpeedBean"))
        {
            StartCoroutine(ApplySpeedBoost(5f, 1.5f));
            Destroy(other.gameObject); // Destroy the special bean
        }
        else if (other.CompareTag("SuperShootBean"))
        {
            StartCoroutine(EnableShootingForDuration(10f)); // Enable shooting for 10 seconds
            Destroy(other.gameObject);
        }
    }


    IEnumerator ApplySpeedBoost(float duration, float boostMultiplier)
    {
        speed *= boostMultiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }



}
