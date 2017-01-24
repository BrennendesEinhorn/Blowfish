using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class StartCountDown : MonoBehaviour 
{
	public GameObject StartCountdown;
	public int MaxCount = 3;
	public float CountDuration = 1f;
	public float AnimationFraction = 0.75f;
	public float MaxScaling = 1.5f;
	public Ease ScalingEase;
	public Ease ShrinkEase;

	private Text countdownText;
	private RectTransform countdownTransform;

	void Start () 
	{
		countdownText = StartCountdown.GetComponentInChildren<Text> ();
		countdownTransform = StartCountdown.GetComponent<RectTransform> ();
		StartCoroutine (ShowCountDown ());
	}
		

	IEnumerator ShowCountDown()
	{
		var rollScript = FindObjectOfType<SimplePlayerMov> ();
		rollScript.enabled = false;
		for (int countdown = MaxCount; countdown >= 0; countdown--) 
		{
			var tween = countdownTransform.DOScale (Vector3.one * MaxScaling, CountDuration * AnimationFraction);
			tween.SetEase (ScalingEase);
			tween.OnComplete (() => 
				{
					var newTween = countdownTransform.DOScale(Vector3.one, CountDuration * (1 - AnimationFraction));
					newTween.SetEase(ShrinkEase);
				}
				);
			countdownText.text = countdown.ToString ();
			yield return new WaitForSeconds (CountDuration);
		}
		StartCountdown.gameObject.SetActive (false);
		rollScript.enabled = true;
		FindObjectOfType<Timer> ().StartTimer ();
	}
}
