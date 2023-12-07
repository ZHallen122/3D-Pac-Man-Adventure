// Author: Qiaoxin Huang

using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerHandler : MonoBehaviour
{
    // When a ghost collides with Pac-Man, load the game over scene
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
