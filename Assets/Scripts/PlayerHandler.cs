using UnityEngine;

namespace HuangQiaoxin.Lab3
{
    public class PlayerHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // When the player hits the exit trigger, notify the EventManager.
            if (other.CompareTag("Exit"))
            {
                EventManager.onGameOver.Invoke();
            }
        }
    }
}