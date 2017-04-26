using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {

    // public string Ending; // This will be the Credits Scene.

    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioSource clipSource;
    public AudioSource backgroundMusic;

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
        yield return new WaitForSeconds(4);
        //SceneManager.exit();
        SceneManager.LoadScene("Credit Scene");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasPlayed)
            {
                backgroundMusic.Pause();
                clipSource.PlayOneShot(clip, volume);
                clipSource.PlayOneShot(clip2, volume);
                clipSource.PlayOneShot(clip3, 0.9f);
                clipSource.PlayOneShot(clip4, volume);

                hasPlayed = true;
            }

            StartCoroutine(WaitAndDie());
            return;
        }

    }
}
