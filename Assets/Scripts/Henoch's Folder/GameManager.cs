using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerMovement player;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
		if (Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
    }
}
