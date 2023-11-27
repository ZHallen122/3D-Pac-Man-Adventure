using UnityEngine;
using UnityEngine.AI;
using lab6Agent;

public class ClydeAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pacmanTransform;
    private GhostState currentState;
    public Vector3 scatterTarget; // lower-left corner
    private float radius = 8f; // 8-dot radius

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GhostState.Chase;
    }

    void Update()
    {
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

        switch (currentState)
        {
            case GhostState.Chase:
                ChasePacman();
                break;
            case GhostState.Scatter:
                Scatter();
                break;
            case GhostState.Frightened:
                // Frightened logic
                break;
        }
    }

    private void ChasePacman()
    {
        agent.SetDestination(pacmanTransform.position);
        ChangeState(GhostState.Chase);
    }

    private void Scatter()
    {
        agent.SetDestination(scatterTarget);
        ChangeState(GhostState.Scatter);
    }

    public void ChangeState(GhostState newState)
    {
        currentState = newState;
    }
}
