using UnityEngine;
using UnityEngine.UI;

namespace HuangQiaoxin.Lab3
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        private int numberOfCoins = 0;

        private void OnEnable()
        {
            EventManager.onUpdateScore.AddListener(IncrementScore);
        }

        // Increment the number of the collected coins and then update the displayed score
        private void IncrementScore()
        {
            numberOfCoins++;
            scoreText.text = "Score: " + numberOfCoins.ToString();
        }
    }
}