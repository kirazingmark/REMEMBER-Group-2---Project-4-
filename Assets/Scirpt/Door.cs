using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool turn = false;
    public bool timerOn = false;
    public float timer = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (turn)
        {
            transform.Rotate(0, 90, 0);
            transform.Translate(0, 0, 1);
            //timerOn = true;
            turn = false;
        }
        if (timerOn)
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            transform.Rotate(0, -90, 0);
            transform.Translate(0, 0, -1);
            timer = 5;
            turn = false;
            timerOn = false;
        }


    }
}
