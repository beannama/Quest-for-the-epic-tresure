using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject player;
    private Vector3 posicionRelativa;

    void Start()
    {
        posicionRelativa = transform.position - player.transform.position;
    }

    void LateUpdate () 
    {
        transform.position = player.transform.position + posicionRelativa;
    }

}
