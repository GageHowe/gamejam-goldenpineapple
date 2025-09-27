using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public int damage = 10;
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Try to get Health component on collided object
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }

        //// Optionally impart momentum - for example:
        //Rigidbody2D targetRb = collision.rigidbody;
        //if (targetRb != null)
        //{
        //    // Add some force on the collided object in projectile's direction
        //    Vector2 forceDirection = rb.velocity.normalized;
        //    float forceMagnitude = 10f;  // Tune this
        //    targetRb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        //}

        //// Destroy projectile after collision
        //Destroy(gameObject);
    }
}
