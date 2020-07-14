using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 30;
    public const float JUMP_TIME_LIMIT = 3.0f;
    public float JUMP_FORCE = 10f;
    public float x = 0;
    public float y = 0;
    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical"); 
        Vector2 dir = new Vector2(x, y);
        if (GroundCheck.IsGrounded(this.GetComponent<BoxCollider2D>(), 1f)){
        ActionTimer.CreateInterval(()=>{
                 Debug.Log("Prestooo");
            }, JUMP_TIME_LIMIT);
        }
        Walk(dir);
    }
// WALK SHOULD HAVE LESS ACCELERATION/WINDUP
    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }
    private void Jump()
    {
       rb.velocity = Vector2.up * JUMP_FORCE;
    }
}
