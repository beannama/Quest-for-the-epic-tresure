using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public int torchCount;
    private bool complete;
    private GameObject door_Controller;

    // Start is called before the first frame update
    void Start()
    {
        torchCount = 0;
        complete = false;
        door_Controller = GameObject.FindWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        if ((torchCount == 2) && !complete)
        {
            door_Controller.GetComponent<Door_Controller>().ChangeState();
            complete = true;
        }
        else if ((torchCount != 2) && complete)
        {
            door_Controller.GetComponent<Door_Controller>().ChangeState();
            complete = false;
        }
    }

    public void IncreaseTorchCount()
    {
        torchCount++;
    }

    public void DecreaseTorchCount()
    {
        torchCount--;
    }
}
