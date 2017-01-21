﻿using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator anim;
    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            anim.SetBool("slash", true);
        }

        else
        {
            anim.SetBool("slash", false);
        }
    }
}

