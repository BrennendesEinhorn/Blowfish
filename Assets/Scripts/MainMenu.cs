using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public bool isStart;
	public bool isQuit; 
	// Use this for initialization
	void Start () {}

	void OnMouse () {
		if (isStart) {
			Application.LoadLevel (1);
		} if (isQuit){
			Application.Quit ();
		}
	}
	void OnMouseUp() {
		if (isQuit) {
			Application.Quit ();
		} if (isStart) {
			Application.LoadLevel (1);
			GetComponent<Renderer>().material.color = Color.cyan;
		}
	
	}

}
