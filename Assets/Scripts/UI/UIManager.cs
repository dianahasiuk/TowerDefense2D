using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    public TMP_Text goldText;
    public TMP_Text healthText;
    public TMP_Text waveText;
    public TMP_Text timerText;

    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Awake() => Instance = this;

    void Start()
    {
        UpdateGold(GameManager.Instance.currentGold);
        UpdateHealth(GameManager.Instance.currentBaseHealth);
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (menuPanel) menuPanel.SetActive(false);
    }

    public void UpdateGold(int amount)
    { if (goldText) goldText.text = "Gold: " + amount; }

    public void UpdateHealth(int amount)
    { if (healthText) healthText.text = "HP: " + amount; }

    public void UpdateWave(int wave)
    { if (waveText) waveText.text = "Round: " + wave; }

    public void UpdateRound(int round)
    { if (waveText) waveText.text = "Round: " + round + "/10"; }

    public void UpdateTimer(int seconds)
    { if (timerText) timerText.text = seconds > 0 ? "Prep: " + seconds + "s" : "BATTLE!"; }

    public void ShowGameOver(bool playerWon)
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        if (gameOverText) gameOverText.text = playerWon ? "ПЕРЕМОГА!" : "ПОРАЗКА!";
    }

    public void ShowMenu(bool show)
    {
        if (menuPanel) menuPanel.SetActive(show);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }
}