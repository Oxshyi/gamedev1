using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    float health = 100;
    float enemySpeed = 1;
    float distanceXFromPlayer; 
    float distanceYFromPlayer; 
    Vector2 enemyPosition; 
    Vector2 playerPosition; 

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
      enemyPosition = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      SetEnemyOnPlayer(new Vector2(GetDistanceXFromPlayer(), GetDistanceYFromPlayer()));
    }
    private void SetEnemyOnPlayer(Vector2 gravitateVector){
        var rb = GetComponent<Rigidbody2D>();
        if (gravitateVector.x <= 50f && gravitateVector.y <= 50f){
            var gravitateToPlayerX = enemyPosition.x -= gravitateVector.x;
            var gravitateToPlayerY = enemyPosition.y -= gravitateVector.y;

            rb.velocity = new Vector2(gravitateToPlayerX * enemySpeed, gravitateToPlayerY * enemySpeed);
        }       
    }
    private float GetDistanceXFromPlayer(){
        return distanceXFromPlayer = GetEnemyPosition().x - GetPlayerPosition().x;
    }
    private float GetDistanceYFromPlayer(){
        return distanceYFromPlayer = GetEnemyPosition().y - GetPlayerPosition().y;
    }
    private Vector2 GetEnemyPosition(){
        enemyPosition = GetComponent<Rigidbody2D>().position;
        return enemyPosition;
    }
    private Vector2 GetPlayerPosition(){
        var player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
        return playerPosition;
    }
}
