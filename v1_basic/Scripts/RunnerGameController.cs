using UnityEngine;

public class RunnerGameController : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Running,
        GameOver
    }

    [Header("Score")]
    [SerializeField] private float distanceMultiplier = 5f;

    public float DistanceScore { get; private set; }
    public int CoinCount { get; private set; }
    public GameState State { get; private set; } = GameState.Menu;

    private void Start()
    {
        StartRun();
    }

    private void Update()
    {
        if (State != GameState.Running)
        {
            return;
        }

        DistanceScore += Time.deltaTime * distanceMultiplier;
    }

    public void StartRun()
    {
        Time.timeScale = 1f;
        DistanceScore = 0f;
        CoinCount = 0;
        State = GameState.Running;
    }

    public void AddCoin()
    {
        if (State != GameState.Running)
        {
            return;
        }

        CoinCount++;
    }

    public void TriggerGameOver()
    {
        if (State == GameState.GameOver)
        {
            return;
        }

        State = GameState.GameOver;
        Time.timeScale = 0f;
    }
}
