using UnityEngine;
using System.Collections;

public class SoundSkript : MonoBehaviour {

    public AudioSource GrasSound;
    public AudioSource JumpSound;
    public AudioSource StoßSound;
    public AudioSource WasserSound;
    

    public void PlayGrasSound ()
    {
        GrasSound.Play();
    }

    public void PlayJumpSound ()
    {
        JumpSound.Play();
    }

    public void PlayStoßSound()
    {
        StoßSound.Play();
    }

    public void PlayWasserSound()
    {
        WasserSound.Play();
    }

    
}
