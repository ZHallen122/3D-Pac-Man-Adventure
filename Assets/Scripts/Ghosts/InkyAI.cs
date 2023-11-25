using lab6Agent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InkyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pacmanTransform;
    public Transform blinkyTransform; // Reference to Blinky's Transform
    private GhostState currentState;
    public int tilesAhead = 2; // Number of tiles ahead of Pac-Man to consider

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase; // Start in the chase state
    }

    void Update()
    {
        switch (currentState)
        {
            case GhostState.Chase:
                ChasePacman();
                break;
            case GhostState.Scatter:
                // Implement scatter logic
                break;
            case GhostState.Frightened:
                // Implement frightened logic
                break;
                // Handle other states
        }
    }

    private void ChasePacman()
    {
        if (pacmanTransform != null && blinkyTransform != null)
        {
            Vector3 targetPosition = CalculateTargetPosition();
            agent.SetDestination(targetPosition);
        }
    }

    private Vector3 CalculateTargetPosition()
    {
        // Calculate the vector from Blinky to 2 tiles ahead of Pac-Man
        Vector3 pacmanDirection = pacmanTransform.forward;
        Vector3 blinkyToPacman = pacmanTransform.position + pacmanDirection * tilesAhead - blinkyTransform.position;

        // Double the vector to get Inky's target
        Vector3 targetPosition = blinkyTransform.position + blinkyToPacman * 2;
        return targetPosition;
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;
        // Handle any setup needed for the new state
    }
}

