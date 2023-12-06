using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using lab6Agent;

public class BaseGhostAI : MonoBehaviour
{
    protected GhostState currentState;
    private Vector3 startPosition;
    public NavMeshAgent agent;
    private Coroutine resetCoroutine;

    private void Awake()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
        }
    }

    public virtual void ChangeState(GhostState newState)
    {
        currentState = newState;

        if (newState == GhostState.Frightened)
        {
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(ResetToStartPosition());
        }
    }

    private IEnumerator ResetToStartPosition()
    {
        // check if NavMesh and enabled before stopping it
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        transform.position = startPosition;

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

        ChangeState(GhostState.Chase);
    }

}
