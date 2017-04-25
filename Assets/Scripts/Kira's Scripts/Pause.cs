using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    public GameObject pausePanel;
    public AudioSource backgroundMusic;
    public AudioSource environmentalSound1;
    public AudioSource environmentalSound2;


    // Use this for initialization
    public void Start () {

        pausePanel.SetActive(false);
        backgroundMusic.Play();
        Time.timeScale = 1;
    }

    public void OnPause() {

        pausePanel.SetActive(true);
        backgroundMusic.Pause();
        Time.timeScale = 0;
    }

    public void OnUnPause() {

        pausePanel.SetActive(false);
        backgroundMusic.Play();
        Time.timeScale = 1;
    }
}
