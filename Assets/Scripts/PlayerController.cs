using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject attackObject;
    public PlayerStateList pState;

    public PlayerRespawn prspwn;

    private GameObject dragonGameObject;
    private GameObject mageGameObject;

    float changeChrTimer;
    float attackTimer;

    [Header("Timers")]
    [SerializeField] float rechargeTime = 5;
    [SerializeField] float attackRechargeTime = 2;

    [Space(5)]

    [Header(" Booleans (need to be changed) ")]
    public bool isOnTrap;
    

    void Start()
    {
        //TODO: CHECK THIS
        prspwn = GetComponent<PlayerRespawn>();


        dragonGameObject = transform.Find("Dragon").gameObject;
        mageGameObject = transform.Find("Mage").gameObject;

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
        if(pState.usingDragon == true)
        {
            dragonGameObject.SetActive(false);
            mageGameObject.SetActive(true);
            pState.usingDragon = false;
        }
        else if (pState.usingDragon == false)
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
