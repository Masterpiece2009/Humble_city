using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public AudioClip popSound;  // Sound that plays when the balloon pops
    private AudioSource audioSource;
    private bool isPopping = false; // Ensure the pop animation only happens once
    public float popAnimationDuration = 0.5f; // Duration of the pop animation
    public float upDownAmount = 0.2f; // Amount to move up and down
    public float upDownSpeed = 1f; // Speed of the up and down motion

    private Vector3 originalPosition;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Store the original position for up and down motion
        originalPosition = transform.position;
    }

    void Update()
    {
        // Apply smooth up and down motion when not popping
        if (!isPopping)
        {
            float upDownOffset = Mathf.Sin(Time.time * upDownSpeed) * upDownAmount;
            transform.position = originalPosition + new Vector3(0, upDownOffset, 0);
        }
    }

    public void PopBalloon()
    {
        if (isPopping) return; // Prevent multiple triggers
        isPopping = true;

        // Stop the ambient sound
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Start the pop animation
        StartCoroutine(PopAnimation());
    }

    private System.Collections.IEnumerator PopAnimation()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        // Gradually scale down the balloon with up and down movement
        while (elapsedTime < popAnimationDuration)
        {
            // Smooth scale down
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / popAnimationDuration);

            // Smooth up and down motion during popping
            float upDownOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2) * upDownAmount;
            transform.position = originalPosition + new Vector3(0, upDownOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Snap scale and position to final values
        transform.localScale = Vector3.zero;
        transform.position = originalPosition;

        // Play the pop sound
        if (popSound != null)
        {
            audioSource.PlayOneShot(popSound);
            Debug.Log("Playing pop sound.");
        }
        else
        {
            Debug.LogWarning("Pop sound is not assigned!");
        }

        // Destroy the balloon after the pop sound finishes
        float destructionDelay = popSound != null ? popSound.length : 0.1f; // Fallback to 0.1s if no sound
        Debug.Log($"Destroying balloon in {destructionDelay} seconds.");
        Destroy(gameObject, destructionDelay);
    }
}