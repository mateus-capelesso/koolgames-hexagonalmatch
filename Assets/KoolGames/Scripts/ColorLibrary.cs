using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace KoolGames.Scripts
{
    [CreateAssetMenu(fileName = "Colors", menuName = "KoolGames/Colors", order = 0)]
    public class ColorLibrary : ScriptableObject
    {
        public List<TypeColor> colors;

        public Color GetColorByType(int type)
        {
            return colors.FirstOrDefault(c => c.colorTypes == (ColorTypes)type).color;
        }
    }

    [Serializable]
    public struct TypeColor
    {
        public ColorTypes colorTypes;
        public Color color;
    }
}
