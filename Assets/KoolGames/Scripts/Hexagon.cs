using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private TriangleCore trianglePrefab;

        private TriangleCore[] triangles;
        private bool isRotating;

        private void Start()
        {
            InitializeHexagon();
        }

        public void InitializeHexagon()
        {
            triangles = new TriangleCore[6];
            
            int[] colorTypes = new int[]  { 0, 1, 2, 3, 4, 5 };
            System.Random rnd = new System.Random();
            rnd.Shuffle(colorTypes);

            float angle = 0f;
            
            for (int i = 0; i < colorTypes.Length; i++)
            {
                TriangleCore triangle = Instantiate(trianglePrefab, transform.position, Quaternion.Euler(-90f, angle, 0f), transform);

                triangle.SetColorType(colorTypes[i], this);
                triangles[i] = triangle;

                angle += 60f;
            }
        }

        public void HexagonClicked()
        {
            if (isRotating) return;

            isRotating = true;
            transform.DORotate(Vector3.up * 60f, 1f, RotateMode.LocalAxisAdd).OnComplete(() =>
            {
                isRotating = false;
            });
        }
    }
}
