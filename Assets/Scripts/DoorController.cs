using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorController : MonoBehaviour
{
    public enum StateEnum
    {
        Open,
        Close
    }

    private SpriteRenderer spriteR;
    public Sprite doorOpen;
    public Sprite doorClose;
    private StateEnum doorState;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        doorState = StateEnum.Close;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        if (doorState == StateEnum.Close)
        {
            spriteR.sprite = doorOpen;
            doorState = StateEnum.Open;
        }
        else if (doorState == StateEnum.Open)
        {
            spriteR.sprite = doorClose;
            doorState = StateEnum.Close;
        }   
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (doorState == StateEnum.Open)
            {
                SceneManager.LoadScene("Lore_Third", LoadSceneMode.Single);
            }
        }
    }
}
