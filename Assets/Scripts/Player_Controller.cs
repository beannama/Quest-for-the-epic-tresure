using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool jump;
    private bool isMage;
    
    public float maxSpeed = 5f;
    public float Speed = 2f;
    public bool  grounded;
    public float jumpPower = 6.5f;

    void Start()
    {
        isMage = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetAxis("Vertical") > 0 && grounded) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
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
        }


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
}
