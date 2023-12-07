// Author: Qiaoxin Huang

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    // Define display text
    public TextMeshProUGUI gameOverText;

    // Load the main game scene when click on restart
    public void onRestart()
    {
        SceneManager.LoadScene(1);
    }

    // Exit the play mode when click quit button
    public void onExit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Start()
    {
        UpdateGameOverText();
    }

    // Update the text when the collectibles are collected
    void UpdateGameOverText()
    {
        if (BeanManager.instance != null)
        {
            if (BeanManager.instance.leftBean == 0)
            {
                gameOverText.text = "You Win!";
            }
            else
            {
                gameOverText.text = "Game Over";
            }
        }
    }
}
