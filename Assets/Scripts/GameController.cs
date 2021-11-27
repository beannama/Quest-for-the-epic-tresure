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

    public GameObject enemies;

    public UIManager canvas;


    string cheat = "";
    // Start is called before the first frame update
    void Start()
    {
        TORCH_GOAL_COUNTER = torches.Count;
        torchCount = 0;
        complete = false;
        player = GameObject.FindWithTag("Player");

        door_Controller = GameObject.FindWithTag("Door");

        enemies = GameObject.Find("Enemies");
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCheat(cheat);


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


        // CHEAT MANAGMENT
        

        if (Input.GetKey(KeyCode.LeftShift)) // EL USUARIO TIENE QUE ESCRIBIR LIGHTSON
        {

            if (Input.GetKeyDown(KeyCode.L))
            {
                cheat = cheat.Insert(cheat.Length, "L");
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                cheat = cheat.Insert(cheat.Length, "I");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                cheat = cheat.Insert(cheat.Length, "G");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                cheat = cheat.Insert(cheat.Length, "H");
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                cheat = cheat.Insert(cheat.Length, "T");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                cheat = cheat.Insert(cheat.Length, "S");
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                cheat = cheat.Insert(cheat.Length, "O");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                cheat = cheat.Insert(cheat.Length, "N");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                cheat = cheat.Insert(cheat.Length, "F");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                cheat = cheat.Insert(cheat.Length, "E");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                cheat = cheat.Insert(cheat.Length, "C");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                cheat = cheat.Insert(cheat.Length, "D");
            }


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log(cheat);

            cheat = "";
        }
    }

    #region CheatCodes


    public void CheckCheat(string cheatcode)
    {
        if(cheatcode == "LIGHTSON")
        {
            Debug.Log("Turning on torches");
            TurnOnAllTorches();
        }
        if (cheatcode == "LIGHTSOFF")
        {
            Debug.Log("Turning off torches");

            TurnOffAllTorches();
        }
        if (cheatcode == "GENOCIDIO")
        {
            Debug.Log("killing all enemies");

            KillAllEnemies();
        }
    }
    public void KillAllEnemies()
    {
        enemies.SetActive(false);
    }

    public void TurnOnAllTorches()
    {
        for (int i = 0; i < torches.Count; i++)
        {
            torches[i].GetComponent<TorchController>().LightTorch();
        }
    }
    #endregion

    #region Life Managment
    public void CheckLife(int life)
    {
        if (life == 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // --------------------------- Game Over Panel ----------------------------------------
            canvas.GameOverPanel();
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
