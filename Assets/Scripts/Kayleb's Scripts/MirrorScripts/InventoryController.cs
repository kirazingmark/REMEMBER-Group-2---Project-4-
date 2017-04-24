using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Search For Objects by Tag?")]
    [SerializeField]
    private bool tagSearch = false;

    [Header("Tags to Search For")]
    [SerializeField]
    private string mirrorPivotTag;
    [SerializeField]
    private string torchPivotTag;
    [SerializeField]
    private string torchLightTag;

    [Header("Player Objects Found/Added")]
    public GameObject mirrorPivot;
    public GameObject torchPivot;
    public GameObject torchLight;
    public Light torchLightSource;

    [Header("Inventory Toggles")]
    [SerializeField]
    private bool haveMirror;
    [SerializeField]
    private bool haveTorch;
    [SerializeField]
    private bool isMirrorOn;
    [SerializeField]
    private bool isTorchOn;

    [Header("Inventory Variables")]
    [SerializeField]
    private float torchOnBrightness;

    [Header("Rotation Variables")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Vector3 enabledRotationE;
    [SerializeField]
    private Vector3 disabledRotationE;

    private Quaternion enabledRotationQ;
    private Quaternion disabledRotationQ;

    [Header("Mirror Rotation Switches")]
    [SerializeField]
    private bool rotateMirrorToEnabled;
    [SerializeField]
    private bool rotateMirrorToDisabled;

    [Header("Torch Rotation Switches")]
    [SerializeField]
    private bool rotateTorchToEnabled;
    [SerializeField]
    private bool rotateTorchToDisabled;

    ///////////  Start Singleton Block  ///////////
    public static InventoryController Instance;
    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else if (Instance != this)
        { Destroy(gameObject); }
    }
    ///////////  End Singleton Block  ///////////

    void Start ()
    {
        SearchForObjects();
        SetQuaternions();
	}
	
	void Update ()
    {
        RotateMirror();
        RotateTorch();
	}

    // Search for objects in scene via tag for references.
    void SearchForObjects()
    {
        mirrorPivot = GameObject.FindGameObjectWithTag(mirrorPivotTag);
        torchPivot  = GameObject.FindGameObjectWithTag(torchPivotTag);
        torchLight  = GameObject.FindGameObjectWithTag(torchLightTag);
        torchLightSource = torchLight.GetComponent<Light>();
    }

    // Convert the enabled and disabled rotations from Euler to Quaternion
    void SetQuaternions()
    {
        enabledRotationQ  = Quaternion.Euler(enabledRotationE);
        disabledRotationQ = Quaternion.Euler(disabledRotationE);
    }

    // Toggles torch on if currently off, and off if currently on.
    public void ToggleMirror()
    {
        if (haveMirror)
        {
            if (isMirrorOn)
            {
                rotateMirrorToEnabled = false;
                rotateMirrorToDisabled = true;
                isMirrorOn = false;
            }
            else if (!isMirrorOn)
            {
                rotateMirrorToEnabled = true;
                rotateMirrorToDisabled = false;
                isMirrorOn = true;
            }
        }    
    }

    // Toggles torch on if currently off, and off if currently on. Also changes light source brightness.
    public void ToggleTorch()
    {
        if (haveTorch)
        {
            if (isTorchOn)
            {
                rotateTorchToEnabled = false;
                rotateTorchToDisabled = true;
                isTorchOn = false;
                torchLightSource.intensity = 0;
            }

            else if (!isTorchOn)
            {
                rotateTorchToEnabled = true;
                rotateTorchToDisabled = false;
                isTorchOn = true;
                torchLightSource.intensity = torchOnBrightness;
            }
        }  
    }

    // For on pickup of mirror to enable toggling and to toggle for first time.
    public void PickUpMirror()
    {
        haveMirror = true;
        ToggleMirror();
    }

    // For on pickup of torch to enable toggling and to toggle for first time.
    public void PickUpTorch()
    {
        haveTorch = true;
        ToggleTorch();
    }

    // Rotates Mirror to appropriate localRotation depending on whether or not it is on/off.
    void RotateMirror()
    {
        if (rotateMirrorToEnabled)
        {
            mirrorPivot.transform.localRotation = Quaternion.RotateTowards(mirrorPivot.transform.localRotation,
                                                                           enabledRotationQ, 
                                                                           rotationSpeed);
        }

        else if (rotateMirrorToDisabled)
        {
            mirrorPivot.transform.localRotation = Quaternion.RotateTowards(mirrorPivot.transform.localRotation,
                                                                           disabledRotationQ,
                                                                           rotationSpeed);
        }
    }

    // Rotates Torch to appropriate localRotation depending on whether or not it is on/off.
    void RotateTorch()
    {
        if (rotateTorchToEnabled)
        {
            torchPivot.transform.localRotation = Quaternion.RotateTowards(torchPivot.transform.localRotation,
                                                                          enabledRotationQ,
                                                                          rotationSpeed);
        }

        else if (rotateTorchToDisabled)
        {
            torchPivot.transform.localRotation = Quaternion.RotateTowards(torchPivot.transform.localRotation,
                                                                          disabledRotationQ,
                                                                          rotationSpeed);
        }
    } 
}