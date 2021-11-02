using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
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
        AttackController attack_Controller = col.gameObject.GetComponent<AttackController>();
        if (col.gameObject.CompareTag("Attack"))
        {
            int torch_num = EventSystem.GetComponent<GameController>().ReturnNumberTorch(gameObject);
            

            if ((torchState == StateEnum.Off) && (attack_Controller.state == AttackController.State.Fire))
            {
                LightTorch();
            }
            else if ((torchState == StateEnum.On) && (attack_Controller.state == AttackController.State.Cold))
            {
                ExtinguishTorch();
            }
        }
    }

    private void LightTorch()
    {
        spriteR.sprite = torchOn;
        torchState = StateEnum.On;
        EventSystem.GetComponent<GameController>().IncreaseTorchCount();
    }
    private void ExtinguishTorch()
    {
        spriteR.sprite = torchOff;
        torchState = StateEnum.Off;
        EventSystem.GetComponent<GameController>().DecreaseTorchCount();
    }
}
