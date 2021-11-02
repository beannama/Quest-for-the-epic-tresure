using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public GameObject flyingAttack;

    public float nextAttack = 5f;
    public float fireRate = 0.5f;
    
    void Update()
    {
        Attack();
    }

    void Attack(){
        
        if (Time.time > nextAttack){
            nextAttack = Time.time + fireRate;
            Fire();
        }
    }

    void Fire(){
        Instantiate(flyingAttack,transform.position, Quaternion.identity);
    }
}
