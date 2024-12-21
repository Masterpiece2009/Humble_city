using UnityEngine;
using TMPro; // لاستيراد TextMeshPro

public class BallController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool hasMoved = false; // To ensure it only moves once
    public float moveSpeed = 2f; // Speed of the ball movement

    // Reference to the TextMeshPro UI Text element
    public TMP_Text scoreText; // استخدام TMP_Text بدلاً من Text

    void Start()
    {
        // Save the initial position
        initialPosition = transform.position;

        // Calculate the target position (6 units on the Z-axis)
        targetPosition = initialPosition + new Vector3(0, 0, 6);

        // تأكد من أن النص فارغ في البداية
        if (scoreText != null)
        {
            scoreText.text = ""; // إخفاء الرسالة في البداية
        }
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

        // Update the TextMeshPro text
        if (scoreText != null)
        {
            scoreText.text = "The ball has been scored!"; // عرض الرسالة في UI باستخدام TextMeshPro
                                                          // Print the scoring message in the console
            Debug.Log("The ball has been scored!");

            // Start coroutine to hide the text after 2 seconds
            StartCoroutine(HideTextAfterDelay(5f));
        }
    }

    // Coroutine to hide the text after a delay
    private System.Collections.IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (scoreText != null)
        {
            scoreText.text = ""; // إخفاء النص
        }
    }
}
