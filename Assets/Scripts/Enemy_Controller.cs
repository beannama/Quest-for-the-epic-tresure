using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public enum StateEnum
    {
        Normal,
        Frozen,
        Stun
    }

    private StateEnum state;
    private SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Just checking change state
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeState(StateEnum.Frozen);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            Debug.Log("enemy attacked!");
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
            case StateEnum.Stun:
                state = StateEnum.Stun;
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
        else if (state == StateEnum.Stun)
        {
            spriteR.color = Color.red;
        }
    }
}
