using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum StateEnum
    {
        Normal,
        Hitted,
        Frozen
    }

    public GameObject attackObject;
    private StateEnum state;
    private SpriteRenderer spriteR;

    public float Timer;
    public float stateRechargeTime = 1f;

    public float spd;

    // Start is called before the first frame update
    void Start()
    {
        spd = 50f;
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Timer <= 0)
        {
            ChangeState(StateEnum.Normal);
            Timer = stateRechargeTime;
        }
        Timer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 direction = col.gameObject.transform.position;

        if (col.gameObject.CompareTag("Attack"))
        {
            CheckAttack(direction, col);
        }
    }

    void ChangeState(StateEnum stateInput)
    {
        switch (stateInput)
        {
            case StateEnum.Frozen:
                state = StateEnum.Frozen;
                break;
            case StateEnum.Normal:
                state = StateEnum.Normal;
                break;
            case StateEnum.Hitted:
                state = StateEnum.Hitted;
                break;
        }
        ChangeColor();
    }

    void ChangeColor()
    {
        if(state == StateEnum.Frozen)
        {
            spriteR.color = Color.blue;
        }
        else if(state == StateEnum.Normal)
        {
            spriteR.color = Color.white;
        }
        else if (state == StateEnum.Hitted)
        {
            spriteR.color = Color.red;
        }
    }

    void CheckAttack(Vector3 attackDirection, Collider2D col)
    {
        Vector3 direction = transform.position - attackDirection;
        AttackController attack_Controller= col.gameObject.GetComponent<AttackController>();

        if (attack_Controller.state == AttackController.State.Fire)
        {
            ChangeState(StateEnum.Hitted);
            //Do
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    //From the left
                    transform.position = new Vector3(transform.position.x + 1f, 0, 0);
                }
                else
                {
                    //From the right
                    transform.position = new Vector3(transform.position.x - 1f, 0, 0);
                }
            }
        }
        else if (attack_Controller.state == AttackController.State.Cold)
        {

            //Do
            // TODO: STOP MOVEMENT FOR X SECS
            ChangeState(StateEnum.Frozen);
        }

    }

}
