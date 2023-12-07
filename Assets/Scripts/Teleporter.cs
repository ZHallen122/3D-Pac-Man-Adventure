// Author: Allen Zhang

using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter;  // Target teleporter to which objects will be teleported
    public float minDistanceToReactivate = 0.3f; // Minimum distance from the target teleporter

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Ghost")) && CanTeleport(other.transform))
        {
            other.transform.position = targetTeleporter.position;
        }
    }

    // Method to check if the object can be teleported based on distance
    private bool CanTeleport(Transform objectTransform)
    {
        float distanceFromTarget = Vector3.Distance(transform.position, objectTransform.position);
        return distanceFromTarget >= minDistanceToReactivate;
    }
}


