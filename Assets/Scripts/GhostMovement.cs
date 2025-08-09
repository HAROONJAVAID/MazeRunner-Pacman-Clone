using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public LayerMask wallLayer;
    private Transform player;

    private Vector3 currentDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDirection = Vector3.forward;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 bestDirection = currentDirection;
        float shortestDistance = Mathf.Infinity;

        foreach (Vector3 dir in directions)
        {
            if (CanMove(dir))
            {
                Vector3 checkPos = transform.position + dir;
                float dist = Vector3.Distance(checkPos, player.position);

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    bestDirection = dir;
                }
            }
        }

        currentDirection = bestDirection;
        Vector3 newPos = transform.position + currentDirection * moveSpeed * Time.fixedDeltaTime;
        transform.position = newPos;
    }

    bool CanMove(Vector3 dir)
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, dir); // cast from center height
        return !Physics.Raycast(ray, 0.6f, wallLayer);
    }
}
