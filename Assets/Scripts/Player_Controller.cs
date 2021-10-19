using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public StateFacing RIGHT_FACING = StateFacing.Right;
    public StateFacing LEFT_FACING = StateFacing.Left;
    public StateUsingCharacter DRAGON_CHARACTER = StateUsingCharacter.Dragon;
    public StateUsingCharacter MAGE_CHARACTER = StateUsingCharacter.Mage;

    public enum StateFacing
    {
        Left,
        Right
    }    
    public enum StateUsingCharacter
    {
        Dragon,
        Mage
    }

    private StateFacing facingState;
    public StateUsingCharacter characterUsingState;

    public GameObject attackObject;

    public Rigidbody2D rb2d;

    private SpriteRenderer spriteR;


    public PlayerStateList pState;

    //public float maxSpeed = 5f;
    //public float Speed = 2f;
    //public float jumpPower = 6.5f;
    public float changeChrTimer, attackTimer;
    public float rechargeTime = 5f;
    public float attackRechargeTime = 2f;
    //public float horizontalMov;


    public bool isOnTrap;
    public bool isGrounded;
    public bool isJumping;


    void Start()
    {
        facingState = RIGHT_FACING;
        characterUsingState = DRAGON_CHARACTER;
        rb2d = GetComponent<Rigidbody2D>();
        //TODO: CHECK THIS
        pState = GetComponent<PlayerStateList>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump") && isGrounded) {
            JumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (changeChrTimer <= 0)
            {
                ChangeCharacterState();
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




    public void GroundPlayer()
    {
        pState.jumping = false;
        
        isGrounded = true;
        isJumping = false;
    }

    public void JumpPlayer()
    {

        pState.jumping = true;

        isGrounded = false;
        isJumping = true;
    }

    public void ChangeLookState(StateFacing stateLooking)
    {
        switch (stateLooking)
        {
            case StateFacing.Left:
                facingState = LEFT_FACING;
                break;
            case StateFacing.Right:
                facingState = RIGHT_FACING;
                break;
        }
    }

    private void ChangeCharacterState()
    {
        GameObject dragonGameObject = transform.Find("Dragon").gameObject;
        GameObject mageGameObject = transform.Find("Mage").gameObject;

        if(characterUsingState == DRAGON_CHARACTER)
        {
            dragonGameObject.SetActive(false);
            mageGameObject.SetActive(true);
            characterUsingState = MAGE_CHARACTER;
        }
        else if (characterUsingState == MAGE_CHARACTER)
        {
            dragonGameObject.SetActive(true);
            mageGameObject.SetActive(false);
            characterUsingState = DRAGON_CHARACTER;

        }
    }

    private void CreateAttack()
    {
        Vector2 parentPosition = new Vector2();
        Quaternion facing = new Quaternion();
        if (facingState == LEFT_FACING)
        {
            parentPosition = new Vector2(transform.position.x - 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 180, 0, 0);
        }
        else if (facingState == RIGHT_FACING)
        {
            parentPosition = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 0, 0, 0);

        }
        Instantiate(attackObject, parentPosition, facing);
    }

    private void DestroyAttack()
    {
        GameObject attackCreated = GameObject.Find("Attack(Clone)");
        Destroy(attackCreated, 0.5f);
    }

}
