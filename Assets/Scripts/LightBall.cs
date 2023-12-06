// Author: Allen Zhang, Qiaoxin Huang

using lab6Agent;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            BaseGhostAI ghostAI = other.GetComponent<BaseGhostAI>();
            if (ghostAI != null)
            {
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
