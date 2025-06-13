using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public float rotationSpeed = 100f; // Degrees per second

    void Update()
    {
        // Rotate around Z axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
