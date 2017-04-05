﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

    public GameObject player;
    public float rotateSpeed = 10;
    public float upDownSpeed = 10;
    public bool invert = false;
   
   

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        CameraPart();
	}

    void CameraPart()
    {
        int invertVal = -1;
        if (invert)
        {
            invertVal = 1;
        }
        //turn left and right; seeing up and down
        float hozTurn = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float verTurn = Input.GetAxis("Mouse Y") * upDownSpeed * Time.deltaTime * invertVal;

        

        //applying the turning
        player.transform.Rotate(0, hozTurn, 0);
        this.gameObject.transform.Rotate(verTurn, 0, 0);

        Vector3 angles = gameObject.transform.eulerAngles;
        if (angles.x > 180.0f)
        {
            angles.x = angles.x - 360.0f;
        }

        if (angles.x > 25)
        {
            //Debug.Log(angles.x);
            angles.x = 25;
            gameObject.transform.eulerAngles = angles;
        }
        if (angles.x < -25)
        {
            //Debug.Log(angles.x);
            angles.x = -25;
            gameObject.transform.eulerAngles = angles;
        }
    }

}

