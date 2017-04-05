using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseScript : MonoBehaviour {

    [Header("Unit Offset Between Worlds")]
    public float worldOffset;

    [Header("Search For Objects by Tag?")]
    public bool tagSearch = false;

    [Header("Objects")]
    public GameObject player;
    public GameObject playerCamera;
    public GameObject phaseCamera;

    [Header("Player Above or Below?")]
    public bool playerAbove = true;

    [Header("Phase Camera Offset")]
    public float phaseCameraOffset;

    void Start ()
    {
        if (tagSearch == true)
        {
            SearchForObjects();
        }

        if (playerAbove)
        {
            phaseCameraOffset = -worldOffset;
        }
        else if (!playerAbove)
        {
            phaseCameraOffset = worldOffset;
        }
    }
	
	void Update ()
    {
        PhaseCameraControl();

        Transition();
	}

    void Transition()
    {
        if (Input.GetKeyDown("q"))
        {
            if (playerAbove)
            {
                player.transform.position = new Vector3(player.transform.position.x,
                                                        player.transform.position.y - worldOffset,
                                                        player.transform.position.z);
                phaseCameraOffset = worldOffset;
                playerAbove = false;
            }
            else if (!playerAbove)
            {
                player.transform.position = new Vector3(player.transform.position.x,
                                                        player.transform.position.y + worldOffset,
                                                        player.transform.position.z);
                phaseCameraOffset = -worldOffset;
                playerAbove = true;
            }
        }
    }

    void SearchForObjects()
    {
        player      = GameObject.FindGameObjectWithTag("Player");
        phaseCamera = GameObject.FindGameObjectWithTag("PhaseCamera");
    }

    void PhaseCameraControl()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x,
                                             player.transform.position.y + phaseCameraOffset,
                                             player.transform.position.z);

        phaseCamera.transform.position = targetPosition;
        phaseCamera.transform.rotation = playerCamera.transform.rotation;
    }
}
