using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float visionRadius;
    public float speed;

    GameObject player;

    Vector3 initialPosition;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        initialPosition = transform.position;
    }

    void Update()
    {
        ChasePlayer();
    }


    void ChasePlayer(){
        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius){
            target = player.transform.position;
        }

        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
    
}
