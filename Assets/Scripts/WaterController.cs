using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterController : MonoBehaviour
{
    private Tilemap tilemap;
    public float freezeTime;
    private float timer;
    private bool startTimer;
    private Color blue;

    // Start is called before the first frame update
    void Start()
    {
        freezeTime = 3;
        tilemap = GetComponent<Tilemap>();
        startTimer = false;
        blue = new Color(0.427451f, 0.6235294f, 0.8313726f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer<= 0 && startTimer)
            {
                Unfreeze();
                startTimer = false;
            }
        if(startTimer) timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            AttackController attack_Controller = col.gameObject.GetComponent<AttackController>();
            if (attack_Controller.state == AttackController.State.Fire)
            {
                Unfreeze();
            }
            else if (attack_Controller.state == AttackController.State.Cold)
            {
                startTimer = true;
                timer = freezeTime;
                Freeze();
            }
        }
    }

    private void Freeze()
    {
        transform.gameObject.tag = "Land";
        tilemap.color = Color.white;
    }
    
    private void Unfreeze()
    {
        transform.gameObject.tag = "Water";
        tilemap.color = blue;
    }
}
