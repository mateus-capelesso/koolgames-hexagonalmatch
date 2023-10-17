using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace KoolGames.Scripts.UI
{
    public class Fade : MonoBehaviour
    {
        public static Fade Instance;

        [SerializeField] private Image fade;

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
            fade.color = Color.black;
        }

        public void FadeIn(Action callback = null)
        {
            fade.DOColor(Color.clear, 0.25f).OnComplete(() => callback?.Invoke());
        }

        public void FadeOut(Action callback = null)
        {
            fade.DOColor(Color.black, 0.25f).OnComplete(() => callback?.Invoke());
        }
    }
}
