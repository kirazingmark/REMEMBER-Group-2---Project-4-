﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger3 : MonoBehaviour
{

    // VARIABLES AND CONSTANTS.
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    private bool open;
    private bool enter;
    private Vector3 defaultRot;
    private Vector3 openRot;

    public AudioClip openDoor;
    public AudioClip closeDoor;
    AudioSource audioPlayBack;

    public PickUpObject Po;

    // Start Function.
    void Start()
    {
        audioPlayBack = GetComponent<AudioSource>();

        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y - DoorOpenAngle, defaultRot.z);
    }

    //Main Function.
    void Update()
    {

        if (open)
        {

            // Open Door.
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else
        {

            // Close Door.
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }


        if(Po.haveKey)
        {
            if (Input.GetButtonDown("PickUp") && enter)
            {
                OpenDoor();
                open = !open;
            }
            else if (Input.GetButtonDown("PickUp") && !enter)
            {
                // Nothing.
            }
            else
            {
                // Nothing.
            }

        }


    }

    void OnGUI()
    {

        if (enter && Po.haveKey == true)
        {

            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 200, 350, 80), "<color=white><size=35>Open/Close - 'X'</size></color>");
        }
        else if (enter && Po.haveKey == false)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 200, 350, 80), "<color=white><size=35>Locked</size></color>");
        }
    }

    //Activate the Main function when player is near the Door.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    //Deactivate the Main function when player is go away from Door.
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }

    void OpenDoor()
    {
        audioPlayBack.PlayOneShot(openDoor, 1.0F);
    }

    void CloseDoor()
    {
        audioPlayBack.PlayOneShot(closeDoor, 1.0F);
    }
}
