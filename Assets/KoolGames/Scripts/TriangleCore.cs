using System;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class TriangleCore : MonoBehaviour
    {
        public ColorLibrary colors;
        
        private Renderer renderer;
        private ColorTypes color;
        
        public void SetColorType(int type)
        {
            color = (ColorTypes)type;

            GetMaterialCopy();
        }

        private void OnMouseDown()
        {
            Debug.Log($"Mouse clicked on triangle");
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