using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int startingGold = 300;
    public int baseHealth = 20;
    public int totalRounds = 10;
    public float preparationTime = 15f;

    [HideInInspector] public int currentGold;
    [HideInInspector] public int currentBaseHealth;
    [HideInInspector] public int currentRound = 0;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public GameState currentState;

    private float prepTimer;

    void Awake()
    {
        Instance = this;
        currentGold = startingGold;
        currentBaseHealth = baseHealth;
    }

void Start() => SetState(GameState.Menu);

    void Update()
    {
        if (currentState == GameState.Preparation)
        {
            prepTimer -= Time.deltaTime;
            UIManager.Instance?.UpdateTimer(Mathf.CeilToInt(prepTimer));
            if (prepTimer <= 0) SetState(GameState.Battle);
        }
    }

    public void SetState(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.Menu:
    if (UIManager.Instance != null)
    {
        UIManager.Instance.ShowMenu(true);
        UIManager.Instance.ShowGameOver(false);
    }
    break;
                currentRound++;
                prepTimer = preparationTime;
                UIManager.Instance?.UpdateRound(currentRound);
                UIManager.Instance?.UpdateWave(currentRound);
                break;
            case GameState.Battle:
                EnemySpawner.Instance?.StartSpawning();
                break;
            case GameState.RoundEnd:
                if (currentRound >= totalRounds)
                    SetState(GameState.GameOver);
                else
                    SetState(GameState.Preparation);
                break;
            case GameState.GameOver:
                gameOver = true;
                UIManager.Instance?.ShowGameOver(currentBaseHealth > 0);
                break;
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UIManager.Instance?.UpdateGold(currentGold);
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UIManager.Instance?.UpdateGold(currentGold);
            return true;
        }
        return false;
    }

    public void DamageBase(int damage)
    {
        currentBaseHealth -= damage;
        UIManager.Instance?.UpdateHealth(currentBaseHealth);
        if (currentBaseHealth <= 0) SetState(GameState.GameOver);
    }

    public void RoundComplete() => SetState(GameState.RoundEnd);
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}