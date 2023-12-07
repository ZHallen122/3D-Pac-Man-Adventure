// Author: Allen Zhang

using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class BlinkyAI : BaseGhostAI
{

    public Transform pacmanTransform;   // Transform of Pacman, the target for Blinky

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

    // Method for Blinky to chase Pacman
    private void ChasePacman()
    {
        if (pacmanTransform != null)
        {
            agent.SetDestination(pacmanTransform.position);
        }
    }

    // Override ChangeState method from BaseGhostAI
    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }

    // Method to update Pacman transform
    public void setPacmanTransform(Transform newPacmanTransform)
    {
        pacmanTransform = newPacmanTransform;
    }
}
