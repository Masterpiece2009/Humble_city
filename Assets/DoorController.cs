using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    public float moveSpeed = 2f; // Speed of the door movement

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, 11, 0); // Target position (10 units on X-axis)
    }

    public void MoveDoor()
    {
        if (!isOpen)
        {
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(SmoothMove(targetPosition)); // Move to open position
            isOpen = true;
        }
        else
        {
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(SmoothMove(initialPosition)); // Move back to initial position
            isOpen = false;
        }
    }

    private IEnumerator SmoothMove(Vector3 destination)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, destination, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed; // Smooth the movement
            yield return null; // Wait for the next frame
        }

        transform.position = destination; // Ensure the door ends exactly at the destination
    }
}
