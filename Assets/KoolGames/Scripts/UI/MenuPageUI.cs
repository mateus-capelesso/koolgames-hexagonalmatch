
using DG.Tweening;
using KoolGames.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPageUI : MonoBehaviour, IUIPage
{
    [SerializeField] private Button startLevelButton;
    [SerializeField] private TMP_Text buttonLabel;

    private void ConfigureButton()
    {
        startLevelButton.onClick.AddListener(InitiateGame);
        buttonLabel.text = LevelManager.Instance.HasLevelSaved() ? $"Continue" : $"Start Game";
    }

    private void InitiateGame()
    {
        startLevelButton.transform.DOPunchScale(Vector3.one * -0.1f, 0.25f).SetEase(Ease.OutBack);
        
        Fade.Instance.FadeOut(() =>
        {
            LevelManager.Instance.InitiateLevel();
            Hide();
            Fade.Instance.FadeIn();
        });
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
        ConfigureButton();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
