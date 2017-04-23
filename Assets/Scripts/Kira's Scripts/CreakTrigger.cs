using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreakTrigger : MonoBehaviour {

    public AudioClip clip;
    public AudioSource clipSource;

    public float volume = 1.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            clipSource.PlayOneShot(clip, volume);

            return;
        }

    }
}