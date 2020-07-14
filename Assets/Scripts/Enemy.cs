using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 100;
    public float enemySpeed = 1;
    public bool hasTarget = false;
    public GameObject playerTarget; 
    public Rigidbody2D rb;

    void Awake()
    {
         rb = gameObject.GetComponent<Rigidbody2D>();
         var vector = new Vector2(100, 100);
         var newVector = new Vector2(11, 77);
         float newX = newVector.x;
         float x = vector.x;
    }

    // Update is called once per frame
    void Update()
    {     
        
        Debug.Log(hasTarget);
        if (hasTarget){
            float distance = Vector3.Distance(transform.position, playerTarget.transform.position);
            Debug.Log(distance);
            if (distance > 2){
                SetEnemyOnPlayer(playerTarget.transform);
            }
        }
    }
    private void SetEnemyOnPlayer(Transform target){
        Debug.Log(rb);
        rb.AddForce((target.transform.position - transform.position).normalized * enemySpeed);
    }
    private void OnTriggerExit2D(Collider2D collision){ 
        if (collision.tag.Equals("Player")){
            playerTarget = null;
            hasTarget = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag.Equals("Player")){
            playerTarget = collision.gameObject;
            hasTarget = true;
        }
    }

}
