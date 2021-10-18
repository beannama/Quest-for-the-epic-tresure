using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public GameObject attackObject;

    private Rigidbody2D rb2d;
    private bool jump;
    private bool isMage;
    
    public float maxSpeed = 5f;
    public float Speed = 2f;
    public bool  grounded;
    public float jumpPower = 6.5f;
    public float Timer, Timer2;
    public float rechargeTime = 5f;
    public float attackRechargeTime = 2f;

    void Start()
    {
        isMage = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump") && grounded) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Timer <= 0){
                Debug.Log("is pressed");
                if (isMage)
                {
                    isMage = false;
                }
                else
                {
                    isMage = true;
                }

                checkIsMage(isMage);
                Timer = rechargeTime;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Timer2 <= 0){
                createAttack();
                destroyAttack();
                Timer2 = attackRechargeTime;
            }
        }
        Timer -= Time.deltaTime;
        Timer2 -= Time.deltaTime;
    }

    void FixedUpdate(){
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * Speed * h);

        float limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limiteSpeed, rb2d.velocity.y);

        if(h > 0.1f){
            transform.localScale = new Vector3(1f,1f,1f);
        }
        if (h < -0.1f){
            transform.localScale = new Vector3(-1f,1f,1f);
        }

        if (jump){
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }

    void checkIsMage(bool isMage)
    {
        GameObject dragonGameObject = transform.Find("Dragon").gameObject;
        GameObject mageGameObject = transform.Find("Mage").gameObject;

        if (isMage)
        {
            dragonGameObject.SetActive(false);
            mageGameObject.SetActive(true);
        }
        else 
        {
            dragonGameObject.SetActive(true);
            mageGameObject.SetActive(false);

        }
    }

    void createAttack()
    {
        Vector2 parentPosition = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
        Debug.Log("attack!");
        GameObject childObject = Instantiate(attackObject, parentPosition, Quaternion.identity);
        //childObject.transform.parent = transform;
    }

    void destroyAttack()
    {
        GameObject attackCreated = GameObject.Find("Attack(Clone)");
        Destroy(attackCreated, 0.5f);
    }
}
