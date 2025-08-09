using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverUI;
    public TMP_Text titleText;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Audio")]
    public AudioSource audioSource; // Assign in Inspector
    public AudioClip gameOverSound; // Assign Game Over sound
    public AudioClip buttonClickSound;

    void Start()
    {
        gameOverUI.SetActive(false);

        // Button events
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        titleText.text = "Game Over";

        // Play Game Over sound if assigned
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnButtonClick()
    {
        // Play click sound
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }
}
