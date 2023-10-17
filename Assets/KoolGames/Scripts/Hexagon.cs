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
        private List<Hexagon> neighbors;
        private HexagonBoard boardController;
        private bool isRotating;
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
            neighbors = new List<Hexagon>();
            
            int[] colorTypes = new int[] { 0, 1, 2, 3, 4, 5 };
            System.Random rnd = new System.Random(LevelManager.Instance.GetSeed());
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

        public void AssignNeighbor(Hexagon neighbor)
        {
            if (!neighbors.Exists(n => n.Id == neighbor.Id))
            {
                neighbors.Add(neighbor);
            }
        }

        public void HexagonClicked()
        {
            if (isRotating) return;

            isRotating = true;
            
            foreach (TriangleCore triangle in triangles)
            {
                triangle.IdentifyMatches();
            }
            
            transform.DORotate(Vector3.up * 60f, 1f, RotateMode.LocalAxisAdd).OnComplete(() =>
            {
                isRotating = false;
            });
        }

        public void ColorMatch(bool activeHexagon, TriangleCore triangle)
        {
            matchedTriangles.Add(triangle);
            if (activeHexagon)
            {
                boardController.NewMatchFound();
            }
            
        }

        public List<ColorTypes> CheckForMissingColors()
        {
            return triangles.Except(matchedTriangles).Select(triangle => triangle.GetColorType()).ToList();
        }

        public bool CheckLoseCondition()
        {
            List<ColorTypes> missingColors = CheckForMissingColors();
            List<ColorTypes> copy = new List<ColorTypes>(missingColors);
            
            foreach(ColorTypes color in copy)
            {
                foreach (var neighbor in neighbors)
                {
                    if (neighbor.CheckForMissingColors().Contains(color))
                    {
                        missingColors.Remove(color);
                    }
                }
            }
            
            return missingColors.Count > 0;
        }
    }
}
