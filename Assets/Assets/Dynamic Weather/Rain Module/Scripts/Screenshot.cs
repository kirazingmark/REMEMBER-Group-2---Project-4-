using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {

    public string folderLocation1;
    public string folderLocation2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("screenshot: Input.GetKeyDown(KeyCode.s)");
            Application.CaptureScreenshot(folderLocation1, 2);

        }

        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("screenshot: Input.GetKeyDown(KeyCode.D)");
            Application.CaptureScreenshot(folderLocation2, 2);

        }
    }
}

