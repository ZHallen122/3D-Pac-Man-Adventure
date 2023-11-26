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
    public int tilesAhead = 4;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase; // Start in the chase state
    }

    // Update is called once per frame
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
        if (pacmanTransform != null)
        {
            Vector3 targetPosition = CalculateTargetPosition();
            agent.SetDestination(targetPosition);
        }
    }

    private Vector3 CalculateTargetPosition()
    {
        // Determine Pac-Man's direction and calculate the target position
        // This can be based on Pac-Man's current velocity or the direction he is facing
        Vector3 pacmanDirection = pacmanTransform.forward;
        Vector3 targetPosition = pacmanTransform.position + pacmanDirection * tilesAhead;
        return targetPosition;
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;
        // Handle any setup needed for the new state
    }
}
