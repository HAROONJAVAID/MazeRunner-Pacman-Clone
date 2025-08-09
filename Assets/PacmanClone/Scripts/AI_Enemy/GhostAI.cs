using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class GhostBaseAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;

    public float updateRate = 0.2f;
    private float timer;

    // Frightened State
    private bool isFrightened = false;
    private float frightenedTimer = 0f;
    // For other scripts to check frightened state
    public bool IsFrightened => isFrightened;
    public bool IsDead => isDead;


    // Respawn
    [Header("Respawn Settings")]
    public Transform homePoint;
    public float respawnDelay = 3f;
    private bool isDead = false;

    // Visuals
    [Header("Visuals")]
    public Material originalMaterial;
    public Material frightenedMaterial;
    private Renderer ghostRenderer;

    private float normalSpeed;
    public float frightenedSpeed = 2f;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ghostRenderer = GetComponentInChildren<Renderer>();
        normalSpeed = agent.speed;

        if (ghostRenderer != null && originalMaterial != null)
            ghostRenderer.material = originalMaterial;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (GhostManager.Instance != null)
            GhostManager.Instance.RegisterGhost(this);
    }

    protected virtual void Update()
    {
        if (isDead) return;

        timer -= Time.deltaTime;

        if (isFrightened)
        {
            frightenedTimer -= Time.deltaTime;
            if (frightenedTimer <= 0f)
                ExitFrightened();
        }

        if (timer <= 0f)
        {
            if (isFrightened)
                UpdateFrightenedDestination();
            else
                UpdateDestination();

            timer = updateRate;
        }
    }

    public void EnterFrightened(float duration)
    {
        if (isDead) return;

        isFrightened = true;
        frightenedTimer = duration;
        agent.speed = frightenedSpeed;

        if (ghostRenderer != null && frightenedMaterial != null)
            ghostRenderer.material = frightenedMaterial;
    }

    public void ExitFrightened()
{
    isFrightened = false;
    agent.speed = normalSpeed;

    if (ghostRenderer != null && originalMaterial != null)
        ghostRenderer.material = originalMaterial;
}


    public void Defeat()
    {
        if (!isFrightened || isDead) return;

        Debug.Log($"{name} was eaten!");
        isDead = true;
        isFrightened = false;

        // Add score
        GameManager.Instance?.AddScore(200);

        // Stop movement & hide renderer
        agent.enabled = false;
        if (ghostRenderer != null)
            ghostRenderer.enabled = false;

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
{
    yield return new WaitForSeconds(respawnDelay);

    if (homePoint == null)
    {
        Debug.LogWarning($"{name} is missing a Home Point!");
        yield break;
    }

    // ✅ Make sure frightened state is OFF before reactivating
    isFrightened = false;

    // Move to home position
    transform.position = homePoint.position;

    isDead = false;

    if (ghostRenderer != null)
        ghostRenderer.enabled = true;

    agent.enabled = true;

    ExitFrightened(); // this will restore material/speed too
}


    protected virtual void UpdateFrightenedDestination()
    {
        if (player == null) return;

        Vector3 away = (transform.position - player.position).normalized * 5f;
        if (NavMesh.SamplePosition(transform.position + away, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    protected abstract void UpdateDestination();
}
