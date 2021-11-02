using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    public PlayerSoundController playerSound;
    public GameObject attackObject;
    public PlayerStateList pState;

    private GameObject dragonGameObject;
    private GameObject mageGameObject;

    float changeChrTimer;
    float attackTimer;
    private int life;

    [Header("Timers")]
    [SerializeField] float rechargeTime = 5;
    [SerializeField] float attackRechargeTime = 2;

    [Space(5)]

    [Header(" Booleans (need to be changed) ")]
    public bool isOnTrap;
    
    public Image timerCircleDragon;
    public Image timerCircleWizard;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        playerSound = GetComponent<PlayerSoundController>();
        //TODO: CHECK THIS
        life = 3;

        dragonGameObject = transform.Find("Dragon").gameObject;
        mageGameObject = transform.Find("Mage").gameObject;

        timerCircleDragon = GameObject.Find("dragon_circle").GetComponent<Image>();
        timerCircleWizard = GameObject.Find("wizard_circle").GetComponent<Image>();

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
        CircleTimer();

        if (Input.GetButtonDown("Fire1"))
        {
            if (attackTimer <= 0){
                playerSound.MakeSound();
                CreateAttack();
                DestroyAttack();
                attackTimer = attackRechargeTime;
            }
        }

        //TODO: DAMAGE CHECKER
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    GainLife();
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ReceiveDamage();
        //}

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

        //FACING ATTACK
        if (pState.lookingRight == false)
        {
            parentPosition = new Vector2(transform.position.x - 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 180, 0, 0);
        }
        else if (pState.lookingRight == true)
        {
            parentPosition = new Vector2(transform.position.x + 1.5f, transform.position.y - 0.5f);
            facing = new Quaternion(0, 0, 0, 0);
        }

        
        GameObject childObject = Instantiate(attackObject, parentPosition, facing);

        //ATK PROPERTY
        if (pState.usingDragon == true)
        {
            childObject.GetComponent<AttackController>().state = AttackController.State.Fire;
        }
        else if (pState.usingDragon == false)
        {
            childObject.GetComponent<AttackController>().state = AttackController.State.Cold;
        }

    }
    void DestroyAttack()
    {
        GameObject attackCreated = GameObject.Find("Attack(Clone)");
        Destroy(attackCreated, 0.5f);
    }

    public void ReceiveDamage()
    {
        life--;
        gameController.CheckLife(life);
    }
    public void GainLife()
    {
        life++;
        gameController.CheckLife(life);
    }
    public void KillPlayer()
    {
        life = 0;
        gameController.CheckLife(life);
    }

    void CircleTimer(){
        Image timerCircle;
        if (pState.usingDragon == true){
            timerCircleDragon.fillAmount = 0;
            timerCircle = timerCircleWizard;
        }
        else{
            timerCircleWizard.fillAmount = 0;
            timerCircle = timerCircleDragon;
        }
        if (changeChrTimer > 0){
            timerCircle.fillAmount = 1-(changeChrTimer/rechargeTime);
        }
        else{
            timerCircle.fillAmount = 1;
        }
    }
}
