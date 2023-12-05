// Author: Qiaoxin Huang

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public void onRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void onExit()
    {
        Application.Quit();
    }

    void Start()
    {
        UpdateGameOverText();
    }

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
                gameOverText.text = "You Lose!";
            }
        }
    }
}
