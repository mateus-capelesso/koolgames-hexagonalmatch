using System;
using DG.Tweening;
using KoolGames.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private MenuPageUI menuPageUI;
    [SerializeField] private EndPageUI endPage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreValue;
    
    private void Start()
    {
        Fade.Instance.FadeIn();
        
        menuPageUI.Show();
        endPage.Hide();

        LevelManager.Instance.OnGameStart += StartGame;
        LevelManager.Instance.OnMatchFound += UpdateScore;
        LevelManager.Instance.OnPlayerWin += SetWinSequence;
        LevelManager.Instance.OnGameOver += SetLoseSequence;
        
        scoreText.gameObject.SetActive(false);
        scoreValue.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        scoreText.gameObject.SetActive(true);
        scoreValue.gameObject.SetActive(true);
    }

    private void UpdateScore(int value)
    {
        scoreValue.text = $"{value}";
    }

    private void SetWinSequence()
    {
        endPage.SetWinLayout();
        endPage.Show();
    }

    private void SetLoseSequence()
    {
        endPage.SetLoseLayout();
        endPage.Show();
    }

    

}
