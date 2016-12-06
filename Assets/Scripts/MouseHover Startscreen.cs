using UnityEngine;
using System.Collections;

public class MouseHoverStartscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.black;
	}
	
	// Update is called once per frame
	void OnMouseEnter () {
		renderer.material.color = Color.white;
	}
	void OnMouseExit() {
		renderer.material.color = Color.black;
	}
}
