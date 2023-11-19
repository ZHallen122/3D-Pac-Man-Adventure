using System.Collections;
using UnityEngine;

namespace HuangQiaoxin.Lab3
{
    public class CoinHandler : MonoBehaviour
    {
        // Variables for spinning and bobbing
        [SerializeField] private Vector3 direction;
        [SerializeField] private float spinningSpeed = 2f;
        [SerializeField] private float bobbingSpeed = 2f;
        [SerializeField] private float bobbingOffset = 0.2f;
        private Vector3 startPosition;

        // Variables for collecting coins
        private bool isCollected = false;
        [SerializeField] private AudioClip coinSound;
        //[SerializeField] private float floatDuration = 2f;

        private void Start()
        {
            // Store the initial position of the coin at the beginning for bobbing
            startPosition = transform.position;
        }

        private void Update()
        {
            Spinning();
            Bobbing();
        }

        // Rotate the coin based on specified axis and speed
        private void Spinning()
        {
            transform.Rotate(direction * spinningSpeed * Time.deltaTime);
        }

        // Update the position of the coin using the sine function on Y axis
        private void Bobbing()
        {
            transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * bobbingSpeed) * bobbingOffset, 0);
        }

        // Check if the player collides with the coin
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") && !isCollected)
            {
                // Update the isCollected flag, play the audio, and start a coroutine
                isCollected = true;
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                StartCoroutine(UpdateStatus());
            }
        }

        private IEnumerator UpdateStatus()
        {
            float totalDistance = 3f;
            float currentDistance = 0f;

            // Float upward for a certain distance
            while(currentDistance < totalDistance)
            {
                float MovementInOneFrame = Time.deltaTime * bobbingSpeed;
                transform.position += new Vector3(0, MovementInOneFrame, 0);
                currentDistance += MovementInOneFrame;
                yield return null;
            }

            // Destroy the coin and then invoke the event of updating score
            Destroy(gameObject);
            EventManager.onUpdateScore.Invoke();
        }
    }
}