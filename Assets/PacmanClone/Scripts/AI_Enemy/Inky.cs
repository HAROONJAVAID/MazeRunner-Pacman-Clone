using UnityEngine;

public class InkyAI : GhostBaseAI
{
    public float flankDistance = 4f;
    protected override void UpdateDestination()
    {
        if (player == null) return;
        Vector3 target = player.position + (player.right + player.forward).normalized * flankDistance;
        agent.SetDestination(target);
    }
}
