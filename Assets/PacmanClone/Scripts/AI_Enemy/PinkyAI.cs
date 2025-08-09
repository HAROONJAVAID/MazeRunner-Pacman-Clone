using UnityEngine;

public class PinkyAI : GhostBaseAI
{
    public float offsetDistance = 3f;
    protected override void UpdateDestination()
    {
        if (player == null) return;
        Vector3 ahead = player.position + player.forward * offsetDistance;
        agent.SetDestination(ahead);
    }
}
