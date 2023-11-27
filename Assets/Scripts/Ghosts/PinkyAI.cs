using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class PinkyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pacmanTransform;
    private GhostState currentState;
    public int predictAhead = 3;
    public float closeDistanceThreshold = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase; // Start in the chase state
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPacman = Vector3.Distance(transform.position, pacmanTransform.position);
        if (distanceToPacman <= closeDistanceThreshold)
        {
            predictAhead = 0;
        }
        else
        {
            predictAhead = 5;
        }

        switch (currentState)
        {
            case GhostState.Chase:
                ChasePacman();
                break;
            case GhostState.Frightened:
                // Implement frightened logic
                break;
        }
    }

    private void ChasePacman()
    {
        Vector3 targetPosition = CalculateTargetPosition();
        if (!agent.pathPending)
        {
            agent.SetDestination(targetPosition);
        }
    }

    private Vector3 CalculateTargetPosition()
    {
        
        Vector3 pacmanDirection = pacmanTransform.forward;
        Vector3 targetPosition = pacmanTransform.position + pacmanDirection * predictAhead;
        return targetPosition;
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;
    }
}
