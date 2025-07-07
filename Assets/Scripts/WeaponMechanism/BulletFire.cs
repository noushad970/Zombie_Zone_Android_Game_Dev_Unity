using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletFire : MonoBehaviour
{
    [SerializeField] private float lifeAfterHit = 2f;     // seconds before arrow despawns
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private ParticleSystem hitFx;
    private Rigidbody2D rb;

    private bool hasHit;

    void Awake(){

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f; // Disable gravity for the bullety
    }
    void FixedUpdate()
    {
        if (!hasHit && rb.linearVelocity.sqrMagnitude > 0.01f)
            transform.right = rb.linearVelocity;  // rotate so +X axis faces travel path;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasHit) return;

        // Is the object tagged "zombie" or "boss"?
        if (col.CompareTag("Zombie"))
        {
            hasHit = true;
            StickInto(col.transform);

            col.GetComponent<EnemyAI>().TakeDamage(50);
            // Example feedback
            Debug.Log($"Bullet hit a {col.tag}!", col.gameObject);
            //if (hitSfx) AudioSource.PlayClipAtPoint(hitSfx, transform.position);
            if (hitFx != null)
            {
                hitFx.Play();
            }
            // TODO: call the target's damage method here, e.g.
            // col.GetComponent<Health>()?.TakeDamage(damageAmount);
        }
        else if (col.CompareTag("Butcher"))
        {
            hasHit = true;
            StickInto(col.transform);
            AudioManager.instance.BossButcherHurtPlay();
            col.GetComponent<EnemyAI>().TakeDamage(50);
            if (hitFx != null)
            {
                hitFx.Play();
            }
            CameraFollow.instance.shakeDuration = .3f;
        }
        else if (col.CompareTag("Ground"))
        {
            StickInto(col.transform);
        }
        Destroy(gameObject, 3);
    }

    private void StickInto(Transform target)
    {
        // Freeze physics so it stops and sticks
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Parent to the target so it moves with it
        transform.SetParent(target);

        // Destroy arrow after a delay to keep the scene clean
        Destroy(gameObject, lifeAfterHit);
    }



}
