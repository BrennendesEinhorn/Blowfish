﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour {

	public Text timerText;
	private float startTime;
	private bool finnished = false;
	private float endTime = 0f;
    string text;

    public float TotalTime
    {
        get
        {
            return Time.time - startTime;
        }
    }

	public float getEndTime()
	{
		return endTime;
	}
	void Start()
	{
		timerText.gameObject.SetActive (false);
	}

	public void StartTimer() 
	{
		timerText.gameObject.SetActive (true);
		startTime = Time.time; 
	}
			
	void Update () 
	{
     
        if (finnished)
			return;
		
		float t = Time.time - startTime;
		int minutes = (int)((int)(t / 60));
		int seconds = (int)(t % 60);
		int milliseconds = (int)((t - (int)t) * 60);


		//milliseconds = (milliseconds % 1000).ToString();
		 text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + milliseconds.ToString("D2") ;
		timerText.text = text;
        endTime = t;
	}

    public string getHighscoreTime()
    {
        return text;
    }

	public void Finnish () {

		finnished = true;
		timerText.color = Color.red;
	}
}
