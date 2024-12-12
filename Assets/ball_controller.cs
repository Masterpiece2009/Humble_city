using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool hasMoved = false; // To ensure it only moves once
    public float moveSpeed = 2f; // Speed of the ball movement

    void Start()
    {
        // Save the initial position
        initialPosition = transform.position;

        // Calculate the target position (11 units on the Z-axis)
        targetPosition = initialPosition + new Vector3(0, 0, 6);
    }

    public void MoveBall()
    {
        // Check if the ball has already moved
        if (!hasMoved)
        {
            hasMoved = true; // Mark as moved
            StartCoroutine(MoveToTarget());
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        // Smoothly move the ball to the target position
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Snap to the target position at the end
        transform.position = targetPosition;

        // Print the scoring message in the console
        Debug.Log("The ball has been scored!");
    }
}
