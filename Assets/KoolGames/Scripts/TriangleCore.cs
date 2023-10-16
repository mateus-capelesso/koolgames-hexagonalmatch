using System;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class TriangleCore : MonoBehaviour
    {
        public ColorLibrary colors;
        
        private Renderer renderer;
        private ColorTypes color;
        private Hexagon hexagonController;
        
        public void SetColorType(int type, Hexagon hexagon)
        {
            hexagonController = hexagon;
            color = (ColorTypes)type;

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

        public void NotifyPossibleMatch(ColorTypes hitColor)
        {
            
            if (hitColor == color)
            {
                gameObject.SetActive(false);
            }
        }

        private void GetMaterialCopy()
        {
            renderer = GetComponent<MeshRenderer>();
            
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                if (!string.Equals(renderer.materials[i].name, $"Color (Instance)", StringComparison.InvariantCultureIgnoreCase)) continue;

                Material material = renderer.materials[i];
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
