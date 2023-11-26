using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class BlinkyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pacmanTransform;
    private GhostState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;
        currentState = GhostState.Chase; // Start in the chase state
    }


    void Update()
    {
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
        if (pacmanTransform != null)
        {
            agent.SetDestination(pacmanTransform.position);
        }
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;
    }

    public void setPacmanTransform(Transform newPacmanTransform)
    {
        pacmanTransform = newPacmanTransform;
    }
}
