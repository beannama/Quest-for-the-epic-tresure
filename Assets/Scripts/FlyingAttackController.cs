using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackController : MonoBehaviour
{
    public Transform player;
    public float speed;
    Rigidbody2D rb2d;
    Vector2 obj;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        obj = ( player.position - transform.position).normalized;

        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        rb2d.velocity = obj * speed ;
        //Vector2 newPos = Vector2.MoveTowards(rb2d.position, obj, speed * Time.deltaTime);
        //rb2d.MovePosition(newPos);
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Land")){
            Destroy(gameObject);
        }
    }
}

            
