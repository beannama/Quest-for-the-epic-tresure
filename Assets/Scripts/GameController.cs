using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int TORCH_GOAL_COUNTER;
    public int torchCount;
    private bool complete;
    private GameObject door_Controller;
    public List<GameObject> hearts;

    // Start is called before the first frame update
    void Start()
    {
        TORCH_GOAL_COUNTER = 3;
        torchCount = 0;
        complete = false;
        door_Controller = GameObject.FindWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        if ((torchCount == TORCH_GOAL_COUNTER) && !complete)
        {
            door_Controller.GetComponent<DoorController>().ChangeState();
            complete = true;
        }
        else if ((torchCount != TORCH_GOAL_COUNTER) && complete)
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
            Destroy(hearts[hearts.Count - 1].gameObject);
            hearts.RemoveAt(hearts.Count -1);
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
