using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoorTrigger : MonoBehaviour {

    // VARIABLES AND CONSTANTS.
    private bool enter;

    public AudioClip blockedDoor;
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
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 500, 80), "<color=white><size=35>Blocked from Outside</size></color>");
        }
    }

    //Activate the Main function when player is near the Door.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            enter = true;
            audioPlayBack.PlayOneShot(blockedDoor, 1.0F);
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