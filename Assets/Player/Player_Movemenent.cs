using UnityEngine;

public class Player_Movemenent : MonoBehaviour
{
    public float movementSpeed = 10;
    public Rigidbody2D body;

    public GameObject projectilePrefab;
    public Transform gunTransform;    // Assign the player's child named "Gun" here
    public float fireCooldown = 0.5f;  // Seconds between shots

    private float lastFireTime;

    void Start()
    {
        lastFireTime = -fireCooldown;
    }

    void FixedUpdate()
    {
        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(inX, inY).normalized * movementSpeed;
        body.AddForce(movement);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the player's Gun position and rotation
        GameObject projectileInstance = Instantiate(projectilePrefab, gunTransform.position, gunTransform.rotation);

        // Add velocity along the Gun's up vector (or forward if 3D)
        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = gunTransform.up * 20f; // Adjust speed here
        }
    }
}
