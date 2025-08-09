using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public TMP_Text livesText; // Assign in Inspector

    private bool isDead = false;
    public GameOverManager gameOverManager; 

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentLives--;

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            UpdateLivesUI();
            Debug.Log("Player lost a life. Remaining: " + currentLives);
        }
    }

    public void GainLife()
    {
        if (isDead) return;

        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateLivesUI();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        UpdateLivesUI();
        gameOverManager.ShowGameOver();
        
        // Optional: Add Game Over UI here
        gameObject.SetActive(false);
        

    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}
