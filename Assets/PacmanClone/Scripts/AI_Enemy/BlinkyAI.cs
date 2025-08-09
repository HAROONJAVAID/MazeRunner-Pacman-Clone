public class BlinkyAI : GhostBaseAI
{
    protected override void UpdateDestination()
    {
        if (player != null)
            agent.SetDestination(player.position);
    }
}
