using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [Tooltip("Duration of the scaling animation")]
    public float animationDuration = 1f;

    private Vector3 targetScale;
    private float animationTimer;

    private void Start()
    {
        // Store the target scale as the current scale of the object
        targetScale = transform.localScale;

        // Start with zero scale
        transform.localScale = Vector3.zero;

        // Start the animation
        StartAnimation();
    }

    public void StartAnimation()
    {
        animationTimer = 0f;
        InvokeRepeating(nameof(UpdateScale), 0f, Time.deltaTime);
    }

    private void UpdateScale()
    {
        // Increment timer
        animationTimer += Time.deltaTime;

        // Calculate normalized time (0 to 1)
        float t = Mathf.Clamp01(animationTimer / animationDuration);

        // Linearly interpolate the scale from zero to target scale
        transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);

        // Stop animation when complete
        if (t >= 1f)
        {
            CancelInvoke(nameof(UpdateScale));
        }
    }

    // Optional method to reset to initial state
    public void ResetAnimation()
    {
        transform.localScale = Vector3.zero;
    }
}