using UnityEngine;
using System.Collections;

public class KugelfischSprungScript : MonoBehaviour 
{
	private Animator anim;
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Jump")) 
		{
			anim.SetTrigger ("test"); 
		}
	}
}
