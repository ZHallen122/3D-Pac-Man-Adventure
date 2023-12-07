// Author: Allen Zhang

using lab6Agent;
using UnityEngine;
using UnityEngine.AI;

public class InkyAI : BaseGhostAI
{
    public Transform pacmanTransform;
    public Transform blinkyTransform; 
    public float tilesAhead = 10f;
    public float blinkyFactor = 2f; // Multiplier for Blinky's influence on Inky's target position

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase;
    }

    void Update()
    {
       
        
        if (pacmanTransform != null && blinkyTransform != null)
        {
            switch (currentState)
            {
                case GhostState.Chase:
                    ChasePacman();
                    break;
                case GhostState.Frightened:
                    break;
            }
        }
    }

    // Method for Inky to chase Pacman
    private void ChasePacman()
    {
        if (agent != null && pacmanTransform != null)
        {
            Vector3 targetPosition = CalculateTargetPosition();
            agent.SetDestination(targetPosition);
        }
    }

    // Method to calculate Inky's target position based on Blinky's positions
    private Vector3 CalculateTargetPosition()
    {
        Vector3 BlinkyDirection = blinkyTransform.forward;
        Vector3 targetPosition = blinkyTransform.position + BlinkyDirection * tilesAhead;

        return targetPosition;
    }

    // Override ChangeState method from BaseGhostAI
    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }
}
