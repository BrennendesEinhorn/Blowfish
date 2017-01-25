using UnityEngine;
using System.Collections;

public class MouseHoverStartscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.black;
	}
	
	// Update is called once per frame
	void OnMouseEnter () {
		GetComponent<Renderer>().material.color = Color.white;
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = Color.black;
	}
}
