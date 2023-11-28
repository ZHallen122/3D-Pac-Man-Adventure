using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanCollider : MonoBehaviour
{
    // When the player collides with the bean, the bean will be destroyed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            Destroy(gameObject);
        }
    }
}
