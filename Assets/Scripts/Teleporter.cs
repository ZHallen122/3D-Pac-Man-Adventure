// Author: Allen Zhang

using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter;
    public float minDistanceToReactivate = 0.3f; // Minimum distance from the target teleporter

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Ghost")) && CanTeleport(other.transform))
        {
            other.transform.position = targetTeleporter.position;
        }
    }

    private bool CanTeleport(Transform objectTransform)
    {
        float distanceFromTarget = Vector3.Distance(transform.position, objectTransform.position);
        return distanceFromTarget >= minDistanceToReactivate;
    }
}


