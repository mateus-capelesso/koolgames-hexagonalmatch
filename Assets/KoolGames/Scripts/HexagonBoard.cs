using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<int, List<ColorTypes>> missingMatches;
        private List<Hexagon> hexagons;

        private readonly List<Vector3> coordinates = new List<Vector3>
        {
            new Vector3(1.75f, 0f, 3f),
            new Vector3(3.5f, 0f, 0f),
            new Vector3(1.75f, 0f, -3f),
            new Vector3(-1.75f, 0f, -3f),
            new Vector3(-3.5f, 0f, 0f),
            new Vector3(-1.75f, 0, 3f)
        };
        

        public void InitializeBoard(int level)
        {
            // Divide by 2 jus to get the int from the operation. Times 2 because we cannot have odd numbers
            numberOfPieces = (level / 2 + 1) * 2;
            
            InitializeRandom();
            InstantiateBoard();
        }

        private void InitializeRandom()
        {
            Random.InitState(LevelManager.Instance.GetSeed());
        }

        private void InstantiateBoard()
        {
            availableSlots = new List<Vector3> { Vector3.zero };
            occupiedSlots = new List<Vector3>();
            hexagons = new List<Hexagon>();
            missingMatches = new Dictionary<int, List<ColorTypes>>();
            
            for (int i = 0; i < numberOfPieces; i++)
            {
                Vector3 position = GetRandomPosition();
                Hexagon hex = Instantiate(hexagonPrefab, position, Quaternion.identity, transform);
                hex.InitializeHexagon(this, i);

                hex.gameObject.name = $"hex - {i}";

                availableSlots.Remove(position);
                occupiedSlots.Add(position);
                
                hexagons.Add(hex);
                
                CheckNewAvailableSlots();
            }

            FixCameraPosition();
        }

        private void FixCameraPosition()
        {
            float width = occupiedSlots.Max(vector => vector.x) - occupiedSlots.Min(vector => vector.x);
            float height = occupiedSlots.Max(vector => vector.z) - occupiedSlots.Min(vector => vector.z);
            float averageWidth = occupiedSlots.Sum(vector => vector.x) / occupiedSlots.Count;
            float averageHeight = occupiedSlots.Sum(vector => vector.z) / occupiedSlots.Count;
            float y = width * 7 / 1.75f;
            
            if (height > 6f)
            {
                y = height * 10 / 5f;
            }
            
            Camera.main.transform.localPosition = new Vector3(averageWidth, y, averageHeight);
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

        public void NewMatchFound()
        {
            int missingCount = 0;
            missingMatches = new Dictionary<int, List<ColorTypes>>();

            LevelManager.Instance.MatchFound();
            
            foreach (Hexagon hex in hexagons)
            {
                missingMatches.Add(hex.Id, hex.CheckForMissingColors());
                missingCount += missingMatches[hex.Id].Count;
            }
            
            if (missingCount == 0)
            {
                Debug.Log($"No more colors left, Player win!");
                LevelManager.Instance.PlayerWin();
            }

            CheckLoseCondition();
        }

        private void CheckLoseCondition()
        {
            foreach (Hexagon hexagon in hexagons)
            {
                if (hexagon.CheckLoseCondition())
                {
                    Debug.Log($"Game Lost");
                    LevelManager.Instance.GameOver();
                    break;
                }
            }
        }
    }
}
