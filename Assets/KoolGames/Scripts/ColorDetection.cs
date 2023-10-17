using System.Collections;
using KoolGames.Scripts;
using UnityEngine;

public class ColorDetection : MonoBehaviour
{
    [SerializeField] private TriangleCore triangle;

    // private bool isDetecting;
    private Coroutine detectionControl;
    private bool blocked = false;

    public void EnableDetection()
    {
        // isDetecting = true;
        // Debug.Log($"Enable detection");

        // detectionControl = StartCoroutine(DeactivateDetection());
    }

    public void BlockCollider()
    {
        blocked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (blocked) return;
        
        if(other.TryGetComponent<TriangleCore>(out TriangleCore matchTriangle))
        {
            ColorTypes otherColor = matchTriangle.GetColorType();
            ColorTypes color = triangle.GetColorType();

            triangle.AssignNeighbor(matchTriangle.GetHexagon());

            if (color == otherColor)
            {
                // isDetecting = false;
                
                matchTriangle.ProcessMatch(false);
                triangle.ProcessMatch(true);
                
                // StopCoroutine(detectionControl);
            }
        }
    }

    // private IEnumerator DeactivateDetection()
    // {
    //     yield return new WaitForSeconds(2.5f);
    //
    //     isDetecting = false;
    // }
}
