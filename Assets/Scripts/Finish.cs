using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour 
{
	private Timer timer;
	void Start () 
	{
		timer = FindObjectOfType<Timer> ();
	}

	private void OnTriggerEnter(Collider other) {

		timer.Finnish ();
	}
}
