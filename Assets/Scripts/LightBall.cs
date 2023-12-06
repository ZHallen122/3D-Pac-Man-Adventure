using lab6Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("test");
            BaseGhostAI ghostAI = other.GetComponent<BaseGhostAI>();
            if (ghostAI != null)
            {
                Debug.Log("test");
                ghostAI.ChangeState(GhostState.Frightened);
            }
            else {
                Debug.Log("Ghost is null");
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        { 
            Destroy(gameObject);
        }
    }
}
