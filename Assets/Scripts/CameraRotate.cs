using UnityEngine;
using UnityEngine.InputSystem;

namespace HuangQiaoxin.Lab3
{
    public class CameraRotate : MonoBehaviour
    {
        private InputAction cameraRotateAction;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float rotateSpeed = 200f;

        public void Initialize(InputAction cameraRotateAction)
        {
            this.cameraRotateAction = cameraRotateAction;

            // Subscribe to the performed event of cameraRotate input action
            cameraRotateAction.performed += CameraRotateAction_performed;
            cameraRotateAction.Enable();
        }

        private void CameraRotateAction_performed(InputAction.CallbackContext obj)
        {
            Vector2 cameraRotateInput = obj.ReadValue<Vector2>();
            var step = rotateSpeed * Time.deltaTime;

            // Calculate a new Y rotation based on action input and update the player's rotation
            float yRotation = playerTransform.eulerAngles.y + cameraRotateInput.x;
            Quaternion playerRotation = Quaternion.Euler(0, yRotation, 0);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, playerRotation, step);

            // Calculate a new X rotation for the camera based on action input and update the camera's rotation
            float xRotation = transform.eulerAngles.x - cameraRotateInput.y;
            Quaternion cameraRotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraRotation, step);
        }
    }
}