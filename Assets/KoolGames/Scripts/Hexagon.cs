using System;
using System.Collections;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private TriangleCore trianglePrefab;

        private TriangleCore[] triangles;
        private Coroutine rotationCoroutine;
        private bool isRotating;

        private void Start()
        {
            InitializeHexagon();
        }

        private void InitializeHexagon()
        {
            triangles = new TriangleCore[6];
            
            int[] colorTypes = new int[]  { 0, 1, 2, 3, 4, 5 };
            System.Random rnd = new System.Random();
            rnd.Shuffle(colorTypes);

            float angle = 0f;
            
            for (int i = 0; i < colorTypes.Length; i++)
            {
                TriangleCore triangle = Instantiate(trianglePrefab, Vector3.zero, Quaternion.Euler(-90f, angle, 0f), transform);

                triangle.SetColorType(colorTypes[i]);
                triangles[i] = triangle;

                angle += 60f;
            }
        }

        public void HexagonClicked()
        {
            if (isRotating) return;

            isRotating = true;
            rotationCoroutine = StartCoroutine(RotateHexagon());
        }

        private IEnumerator RotateHexagon()
        {
           float target = transform.rotation.eulerAngles.y + 60f;
           yield return new WaitForEndOfFrame(); 
        }
    }
}
