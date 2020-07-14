using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 moveDir;
    private const float MOVE_SPEED = 30f;
    private const float JUMP_VELOCITY = 10f;
    private bool leftOrRight;
    private Rigidbody2D rigidbody2DObject;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2DObject = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Move(){
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (GroundCheck.IsGrounded(gameObject.GetComponent<BoxCollider2D>(), 1f)){
                Jump();
            }
        }
    }
    public void Jump(){
        rigidbody2DObject.velocity = Vector2.up * JUMP_VELOCITY;
    }

}
