using System;
using DG.Tweening;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class TriangleCore : MonoBehaviour
    {
        public ColorLibrary colors;
        
        [SerializeField] private ColorDetection detection;
        
        private Renderer meshRenderer;
        private ColorTypes color;
        private Hexagon hexagonController;
        
        
        public void SetColorType(int type, Hexagon hexagon)
        {
            hexagonController = hexagon;
            color = (ColorTypes)type;

            gameObject.name = $"{color}";

            GetMaterialCopy();
        }

        public ColorTypes GetColorType()
        {
            return color;
        }

        private void OnMouseDown()
        {
            hexagonController.HexagonClicked();
        }

        public void NotifyMatch()
        {
            hexagonController.ColorMatch(this);
        }

        public void ProcessMatch()
        {
            transform.DOScale(Vector3.zero, 1f).SetDelay(1f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void IdentifyMatches()
        {
            detection.EnableDetection();
        }

        private void GetMaterialCopy()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (!string.Equals(meshRenderer.materials[i].name, $"Color (Instance)", StringComparison.InvariantCultureIgnoreCase)) continue;

                Material material = meshRenderer.materials[i];
                material.SetColor("_Color", GetColorByColorType());
                
                break;
            }
        }

        private Color GetColorByColorType()
        {
            return colors.GetColorByType((int)color);
            
        }
    }
}
