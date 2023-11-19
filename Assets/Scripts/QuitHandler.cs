using UnityEngine;
using UnityEngine.InputSystem;

namespace HuangQiaoxin.Lab2
{
    public class QuitHandler
    {
        // Subscribe to the performed event of the input action and enables it
        public QuitHandler(InputAction quitAction)
        {
            quitAction.performed += QuitAction_performed; 
            quitAction.Enable();
        }

        // Stop plaing in the Editor and quit the application
        private void QuitAction_performed(InputAction.CallbackContext obj)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}