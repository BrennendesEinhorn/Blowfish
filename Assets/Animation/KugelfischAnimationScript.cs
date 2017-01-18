using UnityEngine;
using System.Collections;

public class KugelfischAnimationScript : MonoBehaviour {

    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void PlayAnimation () {
        anim.SetTrigger("test");
    }
}

