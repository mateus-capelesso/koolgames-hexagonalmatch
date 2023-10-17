using System.Collections;
using KoolGames.Scripts;
using UnityEngine;

public class ColorDetection : MonoBehaviour
{
    [SerializeField] private TriangleCore triangle;

    private bool isDetecting;

    public void EnableDetection()
    {
        isDetecting = true;
        // Debug.Log($"Enable detection");

        // StartCoroutine(DeactivateDetection());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDetecting) return;
        
        if(other.TryGetComponent<TriangleCore>(out TriangleCore matchTriangle))
        {
            ColorTypes otherColor = matchTriangle.GetColorType();
            ColorTypes color = triangle.GetColorType();

            if (color == otherColor)
            {
                isDetecting = false;
                
                triangle.NotifyMatch();
                matchTriangle.ProcessMatch();
                triangle.ProcessMatch();
            }
        }
    }

    private IEnumerator DeactivateDetection()
    {
        yield return new WaitForSeconds(1.5f);
        // Debug.Log($"Disable detection");
        isDetecting = false;
    }
}
