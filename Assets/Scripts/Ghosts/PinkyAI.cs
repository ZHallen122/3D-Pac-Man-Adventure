using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class PinkyAI : BaseGhostAI
{
    public Transform pacmanTransform;
    public int predictAhead = 3;
    public float closeDistanceThreshold = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase;
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
                break;
        }
    }

    private void ChasePacman()
    {
        Vector3 targetPosition = CalculateTargetPosition();
        if (agent!= null && !agent.pathPending)
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

    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }
}
