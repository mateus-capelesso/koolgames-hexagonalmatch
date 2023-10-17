using KoolGames.Scripts;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private HexagonBoard board;
    
    private string partialSeed = "kool-test";
    private string levelKey = "levels";
    private string scoreKey = "levels";
    
    private int currentLevel;
    private int totalScore;

    private void Start()
    {
        currentLevel = PlayerPrefs.HasKey(levelKey) ? PlayerPrefs.GetInt(levelKey) : 3;
        totalScore = PlayerPrefs.HasKey(scoreKey) ? PlayerPrefs.GetInt(scoreKey) : 0;

        InitiateLevel();
    }
    
    private void InitiateLevel()
    {
        string seed = $"{partialSeed}-{currentLevel}";
        board.InitializeBoard(seed, currentLevel);

        board.OnMatchFound += MatchFound;
        board.OnPlayerWin += PlayerWin;
    }

    private void GameOver()
    {
        ClearBoard();
        SaveData();
    }

    private void PlayerWin()
    {
        ClearBoard();
        currentLevel++;
        SaveData();
    }

    private void MatchFound()
    {
        totalScore++;
    }

    private void ClearBoard()
    {
        board.OnMatchFound -= MatchFound;
        board.OnPlayerWin -= PlayerWin;
        
        Destroy(board.gameObject);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(levelKey, currentLevel);
        PlayerPrefs.SetInt(scoreKey, totalScore);
    }
}
