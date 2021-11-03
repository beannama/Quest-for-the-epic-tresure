using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public void OptionsPanel(){
        Time.timeScale = 0;
        optionsPanel.SetActive(true);
    }

    public void Menu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroMenu");
    }

    public void Exit(){
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroMenu", LoadSceneMode.Single);
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
