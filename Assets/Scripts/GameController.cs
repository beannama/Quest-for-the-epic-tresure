using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int torchCount;
    private bool complete;
    private GameObject door_Controller;
    public GameObject[] hearts;

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
        if ((torchCount == 3) && !complete)
        {
            door_Controller.GetComponent<DoorController>().ChangeState();
            complete = true;
        }
        else if ((torchCount != 3) && complete)
        {
            door_Controller.GetComponent<DoorController>().ChangeState();
            complete = false;
        }
    }

    public void CheckLife(int life)
    {
        if (life == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(hearts[hearts.Length - 1].gameObject);
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
