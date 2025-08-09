using UnityEngine;

public class Berry : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GainLife();
            }

            Destroy(gameObject); // Remove berry
        }
    }
}
