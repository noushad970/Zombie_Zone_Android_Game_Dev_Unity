using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;    
    public Transform target;           // The object to follow
    public Vector3 offset;             // Offset of the camera from the target
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    
    // Bound variables
    public Vector2 minBounds;          // Minimum x and y values for the camera
    public Vector2 maxBounds;          // Maximum x and y values for the camera

    // Shake effect variables
    public float shakeDuration = 0f;   // How long the camera should shake
    public float shakeMagnitude = 0.2f;// Intensity of the shake
    private void Awake()
    {
        instance = this;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = target.position + offset;

            // Apply camera shake if active
            if (shakeDuration > 0)
            {
                desiredPosition += Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime;
            }

            // Clamp the position within bounds
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position
            transform.position = smoothedPosition;
        }
    }

    // Public method to trigger the shake
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
