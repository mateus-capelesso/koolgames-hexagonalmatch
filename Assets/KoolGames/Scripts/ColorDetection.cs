using System.Collections;
using KoolGames.Scripts;
using UnityEngine;

public class ColorDetection : MonoBehaviour
{
    [SerializeField] private TriangleCore triangle;

    private bool isDetecting;
    private Coroutine detectionControl;
    private bool blocked = false;

    public void EnableDetection()
    {
        isDetecting = true;
        // Debug.Log($"Enable detection");

        detectionControl = StartCoroutine(DeactivateDetection());
    }

    public void BlockCollider()
    {
        blocked = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDetecting || blocked) return;
        
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
                
                StopCoroutine(detectionControl);
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
