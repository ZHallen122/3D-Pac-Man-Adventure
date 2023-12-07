// Author: Allen Zhang
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using lab6Agent;

public class BaseGhostAI : MonoBehaviour
{
    protected GhostState currentState;  // Current state of the Ghost
    private Vector3 startPosition;  // Starting position of the Ghost
    public NavMeshAgent agent;  // Reference to the NavMeshAgent component
    private Coroutine resetCoroutine;   // Coroutine for resetting the Ghost's position

    private void Awake()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
        }
    }

    // Method to change the state of the Ghost
    public virtual void ChangeState(GhostState newState)
    {
        currentState = newState;

        // When the Ghost becomes frightened, start resetting its position
        if (newState == GhostState.Frightened)
        {
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(ResetToStartPosition());
        }
    }

    // Coroutine to reset the Ghost's position
    private IEnumerator ResetToStartPosition()
    {
        // check if NavMesh and enabled before stopping it
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // Reset position to the starting position
        transform.position = startPosition;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Re-enable the agent
        if (agent != null)
        {
            agent.enabled = true;
            if (agent.isOnNavMesh)
            {
                agent.isStopped = false;
            }
            else
            {
                Debug.LogError("Agent is not on NavMesh");
            }
        }

        // Change state back to chase
        ChangeState(GhostState.Chase);
    }

}
