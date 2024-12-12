using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeHighlighterV2 : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;

    // Define colors for the base and highlighted states
    public Color baseColor = Color.white; // The cube's initial color
    public Color highlightColor = Color.red; // Color when highlighted

    private void Start()
    {
        // Get the renderer component and set the initial color
        cubeRenderer = GetComponent<Renderer>();
        originalColor = baseColor;
        cubeRenderer.material.color = originalColor; // Set initial color to blue
    }

    // Method called when the cube is highlighted (hovered over)
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        cubeRenderer.material.color = highlightColor; // Change to red on hover
    }

    // Method called when the highlight (hover) ends
    public void OnHoverExited(HoverExitEventArgs args)
    {
        cubeRenderer.material.color = originalColor; // Revert back to blue
    }
}