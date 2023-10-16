using KoolGames.Scripts;
using UnityEngine;

public class ColorDetection : MonoBehaviour
{
    [SerializeField] private TriangleCore triangle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<TriangleCore>(out TriangleCore matchTriangle))
        {
            Debug.Log($"Detected possible match");
            
            ColorTypes color = matchTriangle.GetColorType();
            
            triangle.NotifyPossibleMatch(color);
            matchTriangle.NotifyPossibleMatch(triangle.GetColorType());
        }
    }
}
