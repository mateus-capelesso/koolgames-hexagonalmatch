using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace KoolGames.Scripts
{
    public class HexagonBoard : MonoBehaviour
    {
        [SerializeField] private Hexagon hexagonPrefab;

        private int numberOfPieces = 8;
        private List<Vector3> availableSlots;
        private List<Vector3> occupiedSlots;

        private readonly List<Vector3> coordinates = new List<Vector3>
        {
            new Vector3(1.75f, 0f, 3f),
            new Vector3(3.5f, 0f, 0f),
            new Vector3(1.75f, 0f, -3f),
            new Vector3(-1.75f, 0f, -3f),
            new Vector3(-3.5f, 0f, 0f),
            new Vector3(-1.75f, 0, 3f)
        };

        private void Start()
        {
            InstantiateBoard();
        }

        private void InstantiateBoard()
        {
            availableSlots = new List<Vector3> { Vector3.zero };
            occupiedSlots = new List<Vector3>();
            
            for (int i = 0; i < numberOfPieces; i++)
            {
                Vector3 position = GetRandomPosition();
                Hexagon hex = Instantiate(hexagonPrefab, position, Quaternion.identity, transform);
                hex.InitializeHexagon();

                availableSlots.Remove(position);
                occupiedSlots.Add(position);
                
                CheckNewAvailableSlots();
            }
        }

        private Vector3 GetRandomPosition()
        {
            return availableSlots[Random.Range(0, availableSlots.Count)];
        }

        private void CheckNewAvailableSlots()
        {
            foreach (Vector3 slot in occupiedSlots)
            {
                foreach (Vector3 coordinate in coordinates)
                {
                    Vector3 possiblePosition = slot + coordinate;
                    if (!availableSlots.Contains(possiblePosition) && !occupiedSlots.Contains(possiblePosition))
                    {
                        availableSlots.Add(slot + coordinate);
                    }
                }
            }
        }

    }
}
