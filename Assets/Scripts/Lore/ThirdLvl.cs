using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdLvl : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("ThirdLevel", LoadSceneMode.Single);
    }
}
