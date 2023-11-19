using UnityEngine;

namespace HuangQiaoxin.Lab2
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras; // A list of cameras that can be set in Unity Editor
        [SerializeField] private Camera defaultCamera; // The default camera that can be set in Unity Editor
        private int index; // Keep track of the currently active camera

        private void Start()
        {
            index = 0;

            // Loop through each camera and disable it.
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = false;
            }

            // Enable the default camera
            defaultCamera.enabled = true;
        }

        public void NextCamera()
        {
            int nextIndex = (index + 1) % cameras.Length; // If reaching at the end, cycle back to the start

            // Enable the next camera
            cameras[nextIndex].enabled = true;

            // Disable the current camera
            cameras[index].enabled = false;

            index = nextIndex; // Update the index of current camera
        }
    }
}