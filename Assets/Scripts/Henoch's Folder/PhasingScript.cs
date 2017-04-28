using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
    public GameObject mirrorOverlay;

    [Header("Player Above or Below?")]
    public bool playerAbove = true;

    [Header("Phase Camera Offset")]
    public float phaseCameraOffset;

    [Header("Is The Player on The Other World?")]
    public bool turnWorld = false;

    public bool turnOn;
	public bool getMirror;

    public bool hasMirrorBeenUsed = false;
    public AudioClip mirrorActivateFirstTime;
    public AudioClip mirrorActivate;
    public AudioClip mirrorDeactive;
    public AudioSource audioPlayBack;

    public Pause pause;
    public MouseLook player1;

    public FirstPersonController fps;

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
        mirrorOverlay.SetActive(false);
    }

    void Update()
    {
        PhaseCameraControl();

        //comment this part so it can work on henoch's portion
        //Transition();
        if(pause.isPaused)
        {
            mirrorOverlay.SetActive(false);
        }
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
		if (Input.GetButtonDown("Mirror") && turnOn == false)
		{
            mirrorOverlay.SetActive(true);
			phaseCamera.SetActive(true);
			turnOn = true;

            if (hasMirrorBeenUsed == false)
            {
                // audioPlayBack.PlayOneShot(mirrorActivateFirstTime, 1.0F); // Doesn't really fit the mood of the Scene.
                audioPlayBack.PlayOneShot(mirrorActivate, 1.0F);
            }
            else if (hasMirrorBeenUsed == true)
            {
                audioPlayBack.PlayOneShot(mirrorActivate, 1.0F);
            }
            hasMirrorBeenUsed = true;
        }
		else if(Input.GetButtonDown("Mirror") && turnOn == true)
		{
            audioPlayBack.PlayOneShot(mirrorDeactive, 1.0F);
            mirrorOverlay.SetActive(false);
			phaseCamera.SetActive(false);
			turnOn = false;
		}
        if(pause.isPaused)
        {
            mirrorOverlay.SetActive(false);
            fps.m_MouseLook.XSensitivity = 0;
            fps.m_MouseLook.YSensitivity = 0;
        }
        if(!pause.isPaused && turnOn == true)
        {
            mirrorOverlay.SetActive(true);
            fps.m_MouseLook.XSensitivity = 5;
            if(fps.m_IsInverted)
            {
                fps.m_MouseLook.YSensitivity = -2;
            }
            else if(!fps.m_IsInverted)
            {
                fps.m_MouseLook.YSensitivity = 2;
            }
        }
        else if (!pause.isPaused && turnOn == false)
            {
            mirrorOverlay.SetActive(false);
            fps.m_MouseLook.XSensitivity = 5;
            if (fps.m_IsInverted)
            {
                fps.m_MouseLook.YSensitivity = -2;
            }
            else if (!fps.m_IsInverted)
            {
                fps.m_MouseLook.YSensitivity = 2;
            }
        }

    }
}
