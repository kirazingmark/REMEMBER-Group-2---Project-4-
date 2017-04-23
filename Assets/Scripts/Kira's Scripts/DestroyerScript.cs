using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {

    // public string Ending; // This will be the Credits Scene.

    public AudioClip clip;
    public AudioSource clipSource;

    public float volume = 1.0f;

    public bool hasPlayed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(5);
        //SceneManager.exit();
        Application.Quit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasPlayed)
            {
                clipSource.PlayOneShot(clip, volume);
                hasPlayed = true;
            }

            StartCoroutine(WaitAndDie());
            return;
        }

    }
}
