using lab6Agent;
using UnityEngine;
using UnityEngine.AI;

public class InkyAI : BaseGhostAI
{
    public Transform pacmanTransform;
    public Transform blinkyTransform; 
    public float tilesAhead = 10f;
    public float blinkyFactor = 2f;

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

    private void ChasePacman()
    {
        if (agent != null && pacmanTransform != null)
        {
            Vector3 targetPosition = CalculateTargetPosition();
            agent.SetDestination(targetPosition);
        }
    }


    private Vector3 CalculateTargetPosition()
    {
        Vector3 BlinkyDirection = blinkyTransform.forward;
        Vector3 targetPosition = blinkyTransform.position + BlinkyDirection * tilesAhead;

        return targetPosition;
    }

    public override void ChangeState(GhostState newState)
    {
        base.ChangeState(newState);
    }
}
