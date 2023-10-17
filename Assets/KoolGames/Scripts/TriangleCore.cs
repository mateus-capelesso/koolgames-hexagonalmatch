using System;
using System.Collections.Generic;
using System.Linq;
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
        private Material colorMaterial;
        
        
        
        public void SetColorType(int type, Hexagon hexagon)
        {
            hexagonController = hexagon;
            color = (ColorTypes)type;

            gameObject.name = $"{color}";

            GetMaterialCopy();
        }

        public Hexagon GetHexagon()
        {
            return hexagonController;
        }

        public void AssignNeighbor(Hexagon neighbor)
        {
            hexagonController.AssignNeighbor(neighbor);
        }

        public ColorTypes GetColorType()
        {
            return color;
        }

        private void OnMouseDown()
        {
            hexagonController.HexagonClicked();
        }

        public void ProcessMatch(bool activeHexagon)
        {
            detection.BlockCollider();
            colorMaterial.DOColor(Color.white, 1f);
            hexagonController.ColorMatch(activeHexagon, this);
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

                colorMaterial = meshRenderer.materials[i];
                colorMaterial.SetColor("_Color", GetColorByColorType());
                
                break;
            }
        }

        private Color GetColorByColorType()
        {
            return colors.GetColorByType((int)color);
            
        }
    }
}
