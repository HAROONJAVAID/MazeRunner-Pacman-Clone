using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // sound clip assign in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);

            // Player ka audio source use karo
            AudioSource playerAudio = other.GetComponent<AudioSource>();
            if (playerAudio != null && collectSound != null)
            {
                playerAudio.PlayOneShot(collectSound);
            }

            gameObject.SetActive(false);
        }
    }
}
