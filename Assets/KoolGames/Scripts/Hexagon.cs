using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
namespace KoolGames.Scripts
{
    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private TriangleCore trianglePrefab;

        private List<TriangleCore> triangles;
        private List<TriangleCore> matchedTriangles;
        private bool isRotating;
        private HexagonBoard boardController;
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }

        public void InitializeHexagon(HexagonBoard board, int hexagonId)
        {
            id = hexagonId;
            boardController = board;
            triangles = new List<TriangleCore>();
            matchedTriangles = new List<TriangleCore>();
            
            int[] colorTypes = new int[]  { 0, 1, 2, 3, 4, 5 };
            System.Random rnd = new System.Random();
            rnd.Shuffle(colorTypes);

            float angle = 0f;
            
            for (int i = 0; i < colorTypes.Length; i++)
            {
                TriangleCore triangle = Instantiate(trianglePrefab, transform.position, Quaternion.Euler(-90f, angle, 0f), transform);

                triangle.SetColorType(colorTypes[i], this);
                triangles.Add(triangle);

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

                foreach (TriangleCore triangle in triangles)
                {
                    triangle.IdentifyMatches();
                }
            });
        }

        public void ColorMatch(TriangleCore triangle)
        {
            matchedTriangles.Add(triangle);

            boardController.NewMatchFound();
        }

        public List<ColorTypes> CheckForMissingColors()
        {
            return triangles.Except(matchedTriangles).Select(triangle => triangle.GetColorType()).ToList();
        }
    }
}
