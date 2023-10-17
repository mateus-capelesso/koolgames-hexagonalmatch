
using System;
using DG.Tweening;
using KoolGames.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPageUI : MonoBehaviour, IUIPage
{
    [SerializeField] private TMP_Text titleComponent;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TMP_Text buttonLabel;
    [SerializeField] private CanvasGroup canvasGroup;

    public Color red;
    public Color green;

    private void Start()
    {
        buttonComponent.onClick.AddListener(CallNewLevel);
        canvasGroup.alpha = 0;
    }

    private void CallNewLevel()
    {
        buttonComponent.transform.DOPunchScale(Vector3.one * -0.1f, 0.25f).SetEase(Ease.OutBack);
        Fade.Instance.FadeOut(() =>
        {
            LevelManager.Instance.InitiateLevel();
            Hide();
            Fade.Instance.FadeIn();
        });
    }

    public void SetWinLayout()
    {
        titleComponent.text = $"Success!";
        buttonComponent.targetGraphic.color = green;
        buttonLabel.text = $"Continue";

    }

    public void SetLoseLayout()
    {
        titleComponent.text = $"Game Over!";
        buttonComponent.targetGraphic.color = red;
        buttonLabel.text = $"Retry";
    }
    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, 0.5f);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
