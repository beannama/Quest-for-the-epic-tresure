using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public float speed;
    public float visionRange;
    public float reverseRange;
    public float shootsRange;
    public float distancePlayer;
    public float speedReverse;
    public Transform player;
    public Rigidbody2D rb2d;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer(){

        distancePlayer = Vector2.Distance(player.position, rb2d.position);

        if (transform.position.y < player.position.y){
            Vector2 obj = new Vector2(player.position.x, player.position.y + reverseRange );
            Vector2 newPos = Vector2.MoveTowards(rb2d.position, obj, speed * Time.deltaTime);
            rb2d.MovePosition(newPos);
        }
        else{
            if (distancePlayer < visionRange && distancePlayer > reverseRange && distancePlayer > shootsRange){
            Vector2 obj = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb2d.position, obj, speed * Time.deltaTime);
            rb2d.MovePosition(newPos);
            }
            else if (distancePlayer < visionRange && distancePlayer > reverseRange && distancePlayer <= shootsRange){
                Vector2 obj = new Vector2(player.position.x, player.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb2d.position, obj, 0 * Time.deltaTime);
                rb2d.MovePosition(newPos);
            }
            else if (distancePlayer < reverseRange){
                Vector2 obj = new Vector2(transform.position.x, player.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb2d.position, obj, speedReverse * Time.deltaTime);
                rb2d.MovePosition(newPos);
            }
        }

        
        
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootsRange);
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.DrawWireSphere(transform.position, reverseRange);
    }
}
