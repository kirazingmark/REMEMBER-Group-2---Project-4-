using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorTrigger2 : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    private bool enter;

    public AudioClip lockedDoor;
    AudioSource audioPlayBack;

    // Start Function.
    void Start()
    {
        audioPlayBack = GetComponent<AudioSource>();
    }

    //Main Function.
    void Update()
    {

    }

    void OnGUI()
    {

        if (enter)
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
            audioPlayBack.PlayOneShot(lockedDoor, 1.0F);
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

}