using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject gameOverPanel;

    // -------------- Panel Options --------------

    public void OptionsPanel(){
        Time.timeScale = 0;
        optionsPanel.SetActive(true);
    }

    public void Close(){
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    public void Exit(){
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroMenu", LoadSceneMode.Single);
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // -------------- Panel Game Over --------------
    public void GameOverPanel(){
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void NewGame(){
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("FirstLevel");
    }
}
