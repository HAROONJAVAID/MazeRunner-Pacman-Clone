using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public TMP_Text scoreTMP; // assign from UI

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (scoreTMP != null)
        {
            scoreTMP.text = "Score : " + score;
        }
        else
        {
            Debug.LogWarning("scoreTMP not assigned in GameManager!");
        }
    }
}
