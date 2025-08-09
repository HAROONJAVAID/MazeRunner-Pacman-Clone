using UnityEngine;

public class ClydeAI : GhostBaseAI
{
    public float chaseDistance = 5f;

    protected override void UpdateDestination()
    {
        if (player == null) return;

        float d = Vector3.Distance(transform.position, player.position);
        if (d > chaseDistance)
        {
            Vector3 rand = Random.insideUnitSphere * 8f;
            rand.y = 0;
            UnityEngine.AI.NavMesh.SamplePosition(transform.position + rand, out var hit, 8f, UnityEngine.AI.NavMesh.AllAreas);
            agent.SetDestination(hit.position);
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }
}
