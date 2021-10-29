using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLvl : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("FirstLvl", LoadSceneMode.Single);
    }
}
