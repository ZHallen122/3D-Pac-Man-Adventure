using UnityEngine.InputSystem;

namespace HuangQiaoxin.Lab2
{
    public class NextCameraHandler
    {
        private CameraSwitcher cameraSwitcher; // Manage camera
        private InputAction cameraSwitchAction; // Detect the action of switching camera

        public NextCameraHandler(InputAction cameraSwitchAction, CameraSwitcher cameraSwitcher)
        {
            this.cameraSwitcher = cameraSwitcher;
            this.cameraSwitchAction = cameraSwitchAction;

            // Subscribe to the perform event of input action
            cameraSwitchAction.performed += CameraSwitchAction_performed; 
            cameraSwitchAction.Enable();
        }

        // call NextCamera method to switch to the next camera
        private void CameraSwitchAction_performed(InputAction.CallbackContext obj)
        {
            cameraSwitcher.NextCamera(); 
        }
    }
}