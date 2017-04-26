using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    public GameObject pausePanel;
    public bool isPaused;
    public AudioSource backgroundMusic;

    // Use this for initialization
    public void Start () {

        isPaused = false;
        pausePanel.SetActive(false);
        backgroundMusic.Play();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused == false)
            {
                OnPause();
            }
            else if (isPaused == true)
            {
                OnUnPause();
            }
        }
    }

    public void OnPause() {

        isPaused = true;
        pausePanel.SetActive(true);
        backgroundMusic.Pause();
        Time.timeScale = 0;
    }

    public void OnUnPause() {

        isPaused = false;
        pausePanel.SetActive(false);
        backgroundMusic.Play();
        Time.timeScale = 1;
    }
}
