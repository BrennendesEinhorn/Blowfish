using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Finish : MonoBehaviour 
{
	private Timer timer;
	public Text FinishText; 
	void Start () 
	{
		FinishText.gameObject.SetActive (false);
		timer = FindObjectOfType<Timer> ();
	}

	private void OnTriggerEnter(Collider other) {

		timer.Finnish ();	
		FinishText.gameObject.SetActive (true);
	

	}
}
