using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public enum StateLooking
    {
        Left,
        Right
    }
    private StateLooking lookingState;

    public GameObject attackObject;

    private Rigidbody2D rb2d;
    private bool jump;

    public enum StateCharacter
    {
        Dragon,
        Mage
    }
    public StateCharacter characterState;
    
    public float maxSpeed = 5f;
    public float Speed = 2f;
    public bool  grounded;
    public float jumpPower = 6.5f;
    public float Timer, Timer2;
    public float rechargeTime = 5f;
    public float attackRechargeTime = 2f;
    public float horizontalMov;
    

    void Start()
    {
        characterState = StateCharacter.Dragon;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Timer <= 0)
            {
                ChangeCharacterState();
                Timer = rechargeTime;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Timer2 <= 0){
                CreateAttack();
                DestroyAttack();
                Timer2 = attackRechargeTime;
            }
        }
        Timer -= Time.deltaTime;
        Timer2 -= Time.deltaTime;
    }

    void FixedUpdate(){
        horizontalMov = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * Speed * horizontalMov);

        float limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limiteSpeed, rb2d.velocity.y);


        if(horizontalMov > 0.1f){
            transform.localScale = new Vector3(1f,1f,1f);
            CheckLook(StateLooking.Right);
        }
        if (horizontalMov < -0.1f){
            transform.localScale = new Vector3(-1f,1f,1f);
            CheckLook(StateLooking.Left);
        }

        if (jump){
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }
    void CheckLook(StateLooking stateLooking)
    {
        switch (stateLooking)
        {
            case StateLooking.Left:
                lookingState = StateLooking.Left;
                break;
            case StateLooking.Right:
                lookingState = StateLooking.Right;
                break;
        }
    }


    void ChangeCharacterState()
    {
        GameObject dragonGameObject = transform.Find("Dragon").gameObject;
        GameObject mageGameObject = transform.Find("Mage").gameObject;

        if(characterState == StateCharacter.Dragon)
        {
            dragonGameObject.SetActive(false);
            mageGameObject.SetActive(true);
            characterState = StateCharacter.Mage;
        }
        else if (characterState == StateCharacter.Mage)
        {
            dragonGameObject.SetActive(true);
            mageGameObject.SetActive(false);
            characterState = StateCharacter.Dragon;

        }
    }

    void CreateAttack()
    {
        Vector2 parentPosition;
        if (lookingState == StateLooking.Left)
        {
            parentPosition = new Vector2(transform.position.x - 1.5f, transform.position.y - 0.5f);
            GameObject childObject = Instantiate(attackObject, parentPosition, Quaternion.identity);

            childObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (lookingState == StateLooking.Right)
        {
            parentPosition = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
            GameObject childObject = Instantiate(attackObject, parentPosition, Quaternion.identity);

            childObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void DestroyAttack()
    {
        GameObject attackCreated = GameObject.Find("Attack(Clone)");
        Destroy(attackCreated, 0.5f);
    }
}
