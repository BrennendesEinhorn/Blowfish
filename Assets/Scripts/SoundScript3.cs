using UnityEngine;
using System.Collections;

public class SoundScript3 : MonoBehaviour
{



    public AudioSource GrasSound;
    public AudioSource JumpSound;
    public AudioSource StoßSound;
    public AudioSource WasserblasenSound;
    public AudioSource GameFinishSound;


    public void PlayGrasSound()
    {
        GrasSound.Play();
    }

    public void PlayJumpSound()
    {
        JumpSound.Play();
    }

    public void PlayStoßSound()
    {
        StoßSound.Play();
    }

    public void PlayWasserblasenSound()
    {
        WasserblasenSound.Play();
    }
    public void PlayGameFinishSound()
    {
        GameFinishSound.Play();
    }


}

