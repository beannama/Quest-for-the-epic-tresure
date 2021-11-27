using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipLore : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject nextSceneloader;
    public GameObject canvas;

    PlayableDirector playableDirector;
    // Update is called once per frame

    private void Start()
    {
        canvas = GameObject.Find("Canvas");

        playableDirector = canvas.GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("SKIP");
            playableDirector.time = playableDirector.duration;
            nextSceneloader.SetActive(true);
        }

    }
}
