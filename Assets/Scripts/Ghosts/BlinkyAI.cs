using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class BlinkyAI : BaseGhostAI
{

    public Transform pacmanTransform;

    void Start()
    {
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

    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }

    public void setPacmanTransform(Transform newPacmanTransform)
    {
        pacmanTransform = newPacmanTransform;
    }
}
