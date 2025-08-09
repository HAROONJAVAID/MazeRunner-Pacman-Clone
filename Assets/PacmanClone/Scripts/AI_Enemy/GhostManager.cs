using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;

    private List<GhostBaseAI> allGhosts = new List<GhostBaseAI>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RegisterGhost(GhostBaseAI ghost)
    {
        if (!allGhosts.Contains(ghost))
            allGhosts.Add(ghost);
    }

    public void EnterFrightenedMode(float duration)
    {
        foreach (var ghost in allGhosts)
        {
            ghost.EnterFrightened(duration);
        }
    }
}
