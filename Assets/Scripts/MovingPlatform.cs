using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{   // This script controls a moving platform that moves back and forth between two points
    public Vector2 StartPosition = new Vector2(0, 0); // Starting position of the platform
    public Vector2 EndPosition = new Vector2(0, 5); // Ending position of the platform

    public float TimePerCycle = 10.0f; // Time for a complete cycle (forward and back)
    public float WaitTime = 2.0f; // Time to wait at each end of the platform's movement

    private Coroutine m_CurrCoroutine;

    private void Start()
    {
        // Set the initial position of the platform even if object not placed there in scene
        transform.position = StartPosition; 
        StartCoroutine(MovePlatform()); // Start the movement coroutine
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            yield return MoveToPosition(EndPosition); // Move from Start to End position
            yield return new WaitForSeconds(WaitTime); // Wait at the end position

            yield return MoveToPosition(StartPosition); // Move back to Start position
            yield return new WaitForSeconds(WaitTime); // Wait at the start position
        }

    }

    private IEnumerator MoveToPosition(Vector2 targetPosition)
    {
        Vector2 initial = transform.position;
        float duration = (TimePerCycle - WaitTime * 2) / 2; // Caculate duration to move to target position
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Give the platform a continous movement from initial to target position
            transform.position = Vector2.Lerp(initial, targetPosition, elapsedTime / duration); 
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure the platform ends at the target position
    }
}
