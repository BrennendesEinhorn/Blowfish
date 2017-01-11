using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Timer : MonoBehaviour {

	public Text timerText;
	private float startTime;
	private bool finnished = false;

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

		timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + milliseconds.ToString("D2") ;
	}

	public void Finnish () {

		finnished = true;
		timerText.color = Color.red; 
	}
}
