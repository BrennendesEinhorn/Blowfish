using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public GameObject quitMenu;
	public GameObject startText;
	public GameObject exitText;

	// Use this for initialization
	void Start () {
		quitMenu.SetActive (false);
	}
	
	public void ExitPress() {
		quitMenu.SetActive(true);
		startText.SetActive(false);
		exitText.SetActive(false);
	}

	public void NoPress(){
		quitMenu.SetActive(false);
			startText.SetActive(true);
				exitText.SetActive(true);
	}

	public void StartLevel() {
		SceneManager.LoadScene(1);
	}

	public void ExitGame(){
		Application.Quit ();
	}
		
}

