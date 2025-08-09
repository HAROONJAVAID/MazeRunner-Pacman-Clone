using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    private GhostBaseAI ghost;

    void Awake()
    {
        ghost = GetComponent<GhostBaseAI>();
        if (ghost == null)
        {
            Debug.LogError($"{name}: GhostBaseAI missing!");
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player")) return;

    Debug.Log($"{name} collided with player. Frightened: {ghost?.IsFrightened}, Dead: {ghost?.IsDead}");

    if (ghost != null && ghost.IsFrightened)
    {
        Debug.Log("Ghost is frightened → Defeat()");
        ghost.Defeat();
    }
    else if (ghost != null && !ghost.IsDead)
    {
        Debug.Log("Ghost is normal → Player takes damage");
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
            health.TakeDamage();
    }
}

}
