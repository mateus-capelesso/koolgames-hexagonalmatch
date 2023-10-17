using System;
using KoolGames.Scripts;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private GameObject board;

    private HexagonBoard currentBoard;
    private string partialSeed = "kool-test";
    private string levelKey = "levels";
    private string scoreKey = "levels";
    
    private int currentLevel;
    private int totalScore;

    public event Action OnGameStart;
    public event Action<int> OnMatchFound;
    public event Action OnPlayerWin;
    public event Action OnGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentLevel = HasLevelSaved() ? PlayerPrefs.GetInt(levelKey) : 0;
        currentLevel = 0;
        totalScore = PlayerPrefs.HasKey(scoreKey) ? PlayerPrefs.GetInt(scoreKey) : 0;
    }

    public bool HasLevelSaved()
    {
        return PlayerPrefs.HasKey(levelKey);
    }

    public int GetSeed()
    {
        var seed = $"{partialSeed}-{currentLevel}";
        return seed.GetHashCode();
    }
    
    public void InitiateLevel()
    {
        ClearBoard();
        currentBoard = Instantiate(board).GetComponent<HexagonBoard>();
        currentBoard.InitializeBoard(currentLevel);
        OnGameStart?.Invoke();
    }

    public void GameOver()
    {
        SaveData();
        OnGameOver?.Invoke();
    }

    public void PlayerWin()
    {
        currentLevel++;
        SaveData();
        OnPlayerWin?.Invoke();
    }

    public void MatchFound()
    {
        totalScore++;
        OnMatchFound?.Invoke(totalScore);
    }

    private void ClearBoard()
    {
        if (currentBoard == null) return;
        Destroy(currentBoard.gameObject);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(levelKey, currentLevel);
        PlayerPrefs.SetInt(scoreKey, totalScore);
    }
}
