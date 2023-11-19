using UnityEngine;

namespace HuangQiaoxin.Lab3
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverPanel;

        // Listen for the onGameOver event
        private void OnEnable()
        {
            EventManager.onGameOver.AddListener(SetGameOverActive);
        }

        // Stop listening for the onGameOver event
        private void OnDisable()
        {
            EventManager.onGameOver.RemoveListener(SetGameOverActive);
        }

        // Set the panel to be activated when the game is over
        private void SetGameOverActive()
        {
            GameOverPanel.SetActive(true);
        }
    }
}