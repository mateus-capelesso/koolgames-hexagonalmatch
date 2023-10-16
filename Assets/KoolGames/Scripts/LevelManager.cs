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
        currentLevel = PlayerPrefs.HasKey(levelKey) ? PlayerPrefs.GetInt(levelKey) : 1;
        totalScore = PlayerPrefs.HasKey(scoreKey) ? PlayerPrefs.GetInt(scoreKey) : 0;

        InitiateLevel();
    }
    
    

    private void InitiateLevel()
    {
        string seed = $"{partialSeed}-{currentLevel}";
        board.InitializeBoard(seed, currentLevel);
    }
}
