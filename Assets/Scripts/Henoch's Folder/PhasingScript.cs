using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasingScript : MonoBehaviour
{

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

    [Header("Is The Player on The Other World?")]
    public bool turnWorld = false;

    public bool turnOn;
	public bool getMirror;

    ///////////  Start Singleton Block  ///////////
    public static PhasingScript Instance;
    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else if (Instance != this)
        { Destroy(gameObject); }
    }
    ///////////  End Singleton Block  ///////////

    void Start()
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

        phaseCamera.SetActive(false);
    }

    void Update()
    {
        PhaseCameraControl();

        //comment this part so it can work on henoch's portion
        //Transition();
		if (getMirror) {
			Toggle ();
		}
       
    }

    public void Transition()
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
                turnWorld = false;
            }

        
        

    }

    void SearchForObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

	void Toggle()
	{
		if (Input.GetKeyDown(KeyCode.Q) && turnOn == false)
		{
			phaseCamera.SetActive(true);
			turnOn = true;
		}
		else if(Input.GetKeyDown(KeyCode.Q) && turnOn == true)
		{
			phaseCamera.SetActive(false);
			turnOn = false;
		}
	}
}
