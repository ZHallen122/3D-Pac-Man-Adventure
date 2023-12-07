// Author: Qiaoxin Huang, Allen Zhang

using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class ClydeAI : BaseGhostAI
{
    // Define variables
    public Transform pacmanTransform;
    public Vector3 scatterTarget; // lower-left corner
    private float radius = 8f; // 8-dot radius

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase;
    }

    void Update()
    {
        // Get the distance between Clyde ghost and Pac-Man
        float distanceToPacman = Vector3.Distance(transform.position, pacmanTransform.position);

        // If it is farther than 8-dot radius away from Pac-Man, chase Pac-Man;
        // Otherwise, head to lower-left corner.
        if (distanceToPacman > radius)
        {
            ChasePacman();
        }
        else
        {
            Scatter();
        }

        // Switchboard based on the current state
        switch (currentState)
        {
            case GhostState.Chase:
                ChasePacman();
                break;
            case GhostState.Scatter:
                Scatter();
                break;
            case GhostState.Frightened:
                break;
        }
    }

    // Method for state of chasing
    private void ChasePacman()
    {
        if (agent != null && pacmanTransform != null && agent.isOnNavMesh)
        {
            agent.SetDestination(pacmanTransform.position);
            ChangeState(GhostState.Chase);
        }
    }

    // Method for state of scattering
    private void Scatter()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(scatterTarget);
            ChangeState(GhostState.Scatter);
        }
    }

    // Method for chaging to a new state
    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }
}
