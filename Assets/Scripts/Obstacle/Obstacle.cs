using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Log the name of the object it collides with
        Debug.Log("Collided with: " + collision.gameObject.name);

    }
}
