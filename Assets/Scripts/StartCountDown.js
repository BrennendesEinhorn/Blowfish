#pragma strict
var guiCountDown: UnityEngine.UI.Text;
var countMax: int;
private var countDown: int;


function Start () {
	//guiCountDown.enabled=true;
	GameStart();
}

function Update () {
}

function GameStart () {
	var blowfish = gameObject.Find("Hauptcharakter (1)");
	var rollScript = blowfish.GetComponent("PlayerMovement");
	//rollScript.enabled = false;
	for (countDown = countMax; countDown>0; countDown --) {
		guiCountDown.text = countDown.ToString();
		yield WaitForSeconds(1);
	}
	guiCountDown.enabled=false;
	//rollScript.enabled = true;
}