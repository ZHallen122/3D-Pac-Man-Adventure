// Author: Allen Zhang

using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class PinkyAI : BaseGhostAI
{
    public Transform pacmanTransform;
    public int predictAhead = 3;    // Variable to predict how many units ahead of Pacman
    public float closeDistanceThreshold = 5f;   // Distance threshold to change Pinky's behavior

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

    // Method for Pinky to chase Pacman
    private void ChasePacman()
    {
        Vector3 targetPosition = CalculateTargetPosition();
        if (agent!= null && !agent.pathPending)
        {
            agent.SetDestination(targetPosition);
        }
    }

    // calculate Pinky's target position based on Pacman's position and direction
    private Vector3 CalculateTargetPosition()
    {
        
        Vector3 pacmanDirection = pacmanTransform.forward;
        Vector3 targetPosition = pacmanTransform.position + pacmanDirection * predictAhead;
        return targetPosition;
    }

    // Override ChangeState method from BaseGhostAI
    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }
}
