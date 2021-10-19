using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Controller : MonoBehaviour
{
    public enum StateEnum
    {
        On,
        Off
    }

    private SpriteRenderer spriteR;
    private GameObject EventSystem;
    public Sprite torchOn;
    public Sprite torchOff;
    private StateEnum torchState;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        EventSystem = GameObject.FindWithTag("GameController");
        torchState = StateEnum.Off;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Attack_Controller attack_Controller= col.gameObject.GetComponent<Attack_Controller>();
        if (col.gameObject.CompareTag("Attack"))
        {
            if ((torchState == StateEnum.Off) && (attack_Controller.state == Attack_Controller.State.Fire))
            {
                spriteR.sprite = torchOn;
                torchState = StateEnum.On;
                EventSystem.GetComponent<Game_Controller>().IncreaseTorchCount();
            }
            else if ((torchState == StateEnum.On) && (attack_Controller.state == Attack_Controller.State.Cold))
            {
                spriteR.sprite = torchOff;
                torchState = StateEnum.Off;
                EventSystem.GetComponent<Game_Controller>().DecreaseTorchCount();
            }
        }
    }
}
