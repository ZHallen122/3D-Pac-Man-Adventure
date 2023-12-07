// Author: Qiaoxin Huang, Allen Zhang

using UnityEngine;

public class BeanCollider : MonoBehaviour
{
    public GameObject particlePrefab;

    // When the player collides with a collectible,
    // increment the score, play interaction sound and show effect, then the collectible will be destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                BeanManager.instance.addBean();
                audioSource.PlayOneShot(audioSource.clip);
            }
            if (particlePrefab != null)
            {
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
