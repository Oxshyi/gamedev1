using System.Runtime.InteropServices;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 30;
    public float jumpForce = 2;
    public float x = 0;
    public float y = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical"); 
        Vector2 dir = new Vector2(x, y);

// CHANGE SO THAT YOU CAN ONLY HOLD JUMP UNTIL ~0.75 SECONDS

        if (Input.GetKey(KeyCode.Space))
            Jump();
        Walk(dir);
    }

// WALK SHOULD HAVE LESS ACCELERATION/WINDUP

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
        rb.velocity += new Vector2(x, y) * jumpForce;

// DEBUG STATEMENTS

        Debug.Log(jumpForce);
        Debug.Log(x);
        Debug.Log(y);

    }
}
