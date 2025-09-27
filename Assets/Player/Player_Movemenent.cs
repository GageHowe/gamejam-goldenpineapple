using UnityEngine;

public class Player_Movemenent : MonoBehaviour
{
    public float movementSpeed = 10;
    public Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(inX, inY).normalized;
        
        // body.linearVelocity = movement;
        body.AddForce(movement);
    }
}
