using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {

    public Camera mainCamera;
    public GameObject carriedObject;
    public bool isCarrying;
    public bool isOpening;
    public bool isTurn;
	public bool haveKey;
    public bool enter;
    public float distance;
    public float smooth;
    public float timer = 5;

    public AudioClip pickup;
    public AudioClip drop;
    public AudioClip teleportFire;
    public AudioClip teleportFireBack;
    AudioSource audioPlayBack;

    // Use this for initialization
    void Start () {

        audioPlayBack = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
      if(isCarrying)
        {
            carry(carriedObject);
            checkDrop();
        }
      else
        {
            PickUp();
        }
        openDoor();
        if(isTurn)
        {
            TeleportBack();
        }
        else
        {
            firePlace();
        }
       
        

        

    }

    void carry(GameObject objects)
    {
        objects.transform.position = Vector3.Lerp(objects.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
    }

    void PickUp()
    {
		if(Input.GetButtonDown("PickUp"))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,10))
            {
                PickUpable p = hit.collider.GetComponent<PickUpable>();
                if(p != null)
                {
                    audioPlayBack.PlayOneShot(pickup, 1.0F);
                    isCarrying = true;
                    carriedObject = p.gameObject;
                    p.rb.isKinematic = true;
                }
            }
        }
    }

    void checkDrop()
    {
		if(Input.GetButtonDown("PickUp"))
        {
            dropObject();
        }
    }

    void dropObject()
    {
        audioPlayBack.PlayOneShot(drop, 1.0F);
        isCarrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    void openDoor()
    {
        if (Input.GetKey(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Door d = hit.collider.GetComponent<Door>();
                if (d != null)
                {
                    isOpening = true;
                    d.timerOn = true;
                    d.turn = true;
                }
            }
        }
    }

    void firePlace()
    {
		if (Input.GetButtonDown("PickUp"))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,10))
            {

                FirePlace fp = hit.collider.GetComponent<FirePlace>();
                if (fp != null)
                {

                    audioPlayBack.PlayOneShot(teleportFire, 1.0F);
                    isTurn = true;
                    PhasingScript.Instance.Transition();
                }
            }
        }
    }

    void TeleportBack()
    {
		if (Input.GetButtonDown("PickUp") && isTurn == true)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,10))
            {
                FirePlace fp = hit.collider.GetComponent<FirePlace>();
                if (fp != null)
                {
                    audioPlayBack.PlayOneShot(teleportFireBack, 1.0F);
                    isTurn = false;
                    PhasingScript.Instance.Transition();
                }
            }
        }
    }

    void OnGUI()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray activateRay = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(activateRay, out hit))
        {
            FirePlace fp = hit.collider.GetComponent<FirePlace>();

            if (fp != null)
            {
                enter = true;
            }
            else
            {
                enter = false;
            }

            if (enter == true)
            {
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 350, 80), "<color=white><size=35>ACTIVATE - 'X'</size></color>");
            }
            else {
                
            }
        }
    }

}
