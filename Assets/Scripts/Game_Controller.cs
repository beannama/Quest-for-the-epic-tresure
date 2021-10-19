using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public int torchCount;
    // Start is called before the first frame update
    void Start()
    {
        torchCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (torchCount == 3)
        {
            Debug.Log("Tres antorchas prendidas.");
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
