using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerMovement player;
    public Pause pause;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(pause.isPaused == false)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                SceneManager.LoadScene("MainMenu");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
       /* if(Input.GetButtonDown("Fire2") && Input.GetButtonDown("Fire3"))
        {
            SceneManager.LoadScene("MainMenu");
        }*/
    }
}
