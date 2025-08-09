using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public float frightenedDuration = 7f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GhostManager.Instance.EnterFrightenedMode(frightenedDuration);
            Destroy(gameObject);
        }
    }
}
