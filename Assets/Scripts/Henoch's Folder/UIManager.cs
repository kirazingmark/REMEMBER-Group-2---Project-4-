using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Text time;
    public Text timeBig;
    public float timer = 0.0f;
    public float pTimer;
    public bool timerCheck = true;
    public bool deleteData;

    // Use this for initialization
    void Start()
    {
        if (deleteData)
            PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCheck != false)
        {
            timer += Time.deltaTime;
            pTimer += Time.deltaTime;
            time.text = "Current TIme = " + timer.ToString("F1");

        }
        timeBig.text = "Previous TIme = " + PlayerPrefs.GetFloat("timer").ToString("F1");

    }

    void OnApplicationQuit()
    {
        timerCheck = false;
        PlayerPrefs.SetFloat("timer", pTimer);
        PlayerPrefs.Save();
    }
}