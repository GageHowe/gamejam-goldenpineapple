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
    }
}
