using UnityEngine;
using UnityEngine.InputSystem;

namespace HuangQiaoxin.Lab2
{
    public class RotateControl : MonoBehaviour
    {
        [SerializeField] private RotateControl maze;
        [SerializeField] private float speed = 20f;
        private bool isRotating = false;
        private Vector3 mazeCenter;

        // Subscribe to the performed event of input action
        public void Initialize(InputAction rotateAction)
        {
            rotateAction.performed += RotateAction_performed; 
        }

        // Update the isRotating flag based on if the user presses the key
        private void RotateAction_performed(InputAction.CallbackContext obj)
        {
            isRotating = !isRotating;
        }

        // Get the maze center at the beginning
        private void Start()
        {
            if (maze != null) { 
                mazeCenter = GetMazeCenter();
            }
        }

        // Rotate the maze around its center when the user presses the rotate key
        private void Update()
        {
            if (isRotating)
            {
                // Rotate the maze around the center
                transform.RotateAround(mazeCenter, Vector3.up, speed * Time.deltaTime); // Vector3.up is shorthand for Vector3(0, 1, 0)
            }
        }

        // Find the center of the maze by parenting
        private Vector3 GetMazeCenter()
        {
            if (transform.childCount == 0)
                return Vector3.zero;

            // Initialize the variables by default
            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minZ = float.MaxValue;
            float maxZ = float.MinValue;

            // Traverse each child of the maze
            for (int i = 0; i < transform.childCount; i++)
            {
                // Get a cell with many blocks
                Transform child = transform.GetChild(i);
                if (child != null)
                {
                    Vector3 pos = child.position;
                    // Update the x and z bound values
                    if (pos.x < minX) minX = pos.x;
                    if (pos.x > maxX) maxX = pos.x;
                    if (pos.z < minZ) minZ = pos.z;
                    if (pos.z > maxZ) maxZ = pos.z;
                }
                
            }

            // Calculate the center according to the x and y bound values
            float centerX = (minX + maxX) / 2;
            float centerZ = (minZ + maxZ) / 2;
            return new Vector3(centerX, 0, centerZ);
        }

    }
}

