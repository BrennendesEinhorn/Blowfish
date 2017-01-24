using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Krabbe : MonoBehaviour {

    public float duration = 2;
    public float delay = 1;


    // Use this for initialization
    IEnumerator Start () {
        // Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
        yield return new WaitForSeconds(delay);

        // Create a new Sequence.
        // We will set it so that the whole duration is 6
        Sequence s = DOTween.Sequence();
        // Add an horizontal relative move tween that will last the whole Sequence's duration
        s.Append(this.transform.DOMoveX(6, duration).SetRelative().SetEase(Ease.InOutQuad));
        // Set the whole Sequence to loop infinitely forward and backwards
        s.SetLoops(-1, LoopType.Yoyo);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
