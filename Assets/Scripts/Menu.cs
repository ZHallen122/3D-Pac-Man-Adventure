// Author: Qiaoxin Huang

using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Load main game scene when click play button
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    // Exit play mode when click quit button 
    public void quitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
