using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
public class UIManager : MonoBehaviour
{

    public Text time;
    public Text timeBig;
	public Image image;

    public float timer = 0.0f;
    public float pTimer;
    public float cTimer;
    public float minute;
    public float pMinute;
    public bool timerCheck = true;
    public bool deleteData;

    List<float> fList = new List<float>();
	public PhasingScript ps;

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
            cTimer += Time.deltaTime;
            time.text = "Time = " + minute.ToString() + "m " + timer.ToString("F1") + "s";

        }
        if (timer > 60)
        {
            timer = 0;
            minute += 1;
        }
        if (cTimer > 60)
        {
            cTimer = 0;
            pMinute += 1;
        }
        timeBig.text = "Best Time = " + PlayerPrefs.GetFloat("minute").ToString("F0") + "m " + PlayerPrefs.GetFloat("cTimer").ToString("F1") + "s";

		if (ps.getMirror == false)
			image.enabled = false;
		if (ps.getMirror)
			image.enabled = true;

        if(Input.GetButtonDown("Fire2") && Input.GetButtonDown("Fire3"))
        {
            
        }
    }

    void OnApplicationQuit()
    {
        timerCheck = false;
        PlayerPrefs.SetFloat("timer", pTimer);
        PlayerPrefs.SetFloat("cTimer", cTimer);
        PlayerPrefs.SetFloat("minute", pMinute);
        PlayerPrefs.Save();
        //saving the time to the list
        fList.Add(cTimer);
        //converting from float list to string list
        List<string> sList = fList.ConvertAll<string>(x => x.ToString());
        //converting from string list to string
        string myString = string.Join(",", sList.ToArray());
        //writing the timer to a folder
        StreamWriter sw = File.AppendText("TextFolder");
        //myString = "thing";
        sw.WriteLine(myString);
        sw.Close();

        // System.IO.File.WriteAllText ("TextFolder", myString);
    }
}
