using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public GameObject attackObject;

    public PlayerStateList pState;

    
    public float changeChrTimer, attackTimer;
    public float rechargeTime = 5f;
    public float attackRechargeTime = 2f;
    public bool isOnTrap;
    

    void Start()
    {
        pState = GetComponent<PlayerStateList>();
        pState.usingDragon = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (changeChrTimer <= 0)
            {
                ChangeUsingCharacterState();
                changeChrTimer = rechargeTime;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (attackTimer <= 0){
                CreateAttack();
                DestroyAttack();
                attackTimer = attackRechargeTime;
            }
        }

        changeChrTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
    }


    void ChangeUsingCharacterState()
    {
        GameObject dragonGameObject = transform.Find("Dragon").gameObject;
        GameObject mageGameObject = transform.Find("Mage").gameObject;

        if(pState.usingDragon == true)
        {
            dragonGameObject.SetActive(false);
            mageGameObject.SetActive(true);
            pState.usingDragon = false;
        }
        else if (pState.usingDragon == true)
        {
            dragonGameObject.SetActive(true);
            mageGameObject.SetActive(false);
            pState.usingDragon = true;
        }
    }

    void CreateAttack()
    {
        Vector2 parentPosition = new Vector2();

        Quaternion facing = new Quaternion();

        if (pState.lookingRight == false)
        {
            parentPosition = new Vector2(transform.position.x - 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 180, 0, 0);
        }
        if (pState.lookingRight == true)
        {
            parentPosition = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 0, 0, 0);
        }

        Instantiate(attackObject, parentPosition, facing);
    }

    void DestroyAttack()
    {
        GameObject attackCreated = GameObject.Find("Attack(Clone)");
        Destroy(attackCreated, 0.5f);
    }

}
