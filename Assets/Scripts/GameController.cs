using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int TORCH_GOAL_COUNTER;
    public int torchCount;
    public List<GameObject> torches;
    private GameObject player;
    
    private bool complete;
    private GameObject door_Controller;
    public List<GameObject> hearts;

    // Start is called before the first frame update
    void Start()
    {
        TORCH_GOAL_COUNTER = torches.Count;
        torchCount = 0;
        complete = false;
        player = GameObject.FindWithTag("Player");

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

    #region Life Managment
    public void CheckLife(int life)
    {
        if (life == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            if(life > ReturnNumberActiveHearts())
            {
                AddHeart();
            }
            else if (life < ReturnNumberActiveHearts())
            {
                RemoveHeart();
            }
        }
    }
    private int ReturnNumberActiveHearts()
    {
        int count = 0;
        for(int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i].activeSelf)
            {
                count++;
            }
        }

        return count;
    }
    public void AddHeart()
    {
        for(int i = 0; i < hearts.Count; i++)
        {
            if (!hearts[i].activeSelf)
            {
                hearts[i].SetActive(true);
                break;
            }
        }
    }
    public void RemoveHeart()
    {
        for (int i = hearts.Count -1; i > 0; i--)
        {
            if (hearts[i].activeSelf)
            {
                hearts[i].SetActive(false);
                break;
            }
        }

    }
    #endregion

    #region TorchManagment


    public bool CheckIfRightTorch(GameObject torch)
    {
        bool returnedBool = false;

        if(torches[torchCount].name == torch.name)
        {
            returnedBool = true;
        }

        return returnedBool;
    }
    public int ReturnNumberTorch(GameObject torch)
    {
        int torchNumber = 0;

        for(int i = 0; i < torches.Count; i++)
        {
            if(torch.name == torches[i].name)
            {
                torchNumber = i;
            }
        }
        return torchNumber;
    }

    public void TurnOffAllTorches()
    {
        for (int i = 0; i < torches.Count; i++)
        {
            torches[i].GetComponent<TorchController>().ExtinguishTorch();
        }
    }
    public void ResetTorchCount()
    {
        torchCount = 0;
    }
    public void IncreaseTorchCount()
    {
        torchCount++;
    }
    public void DecreaseTorchCount()
    {
        if (torchCount != 0){
            torchCount--;
        }
        
    }
    #endregion
}
