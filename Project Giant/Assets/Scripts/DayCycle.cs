﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    private float timer;
    private bool saved;
    // Start is called before the first frame update
    void Start()
    {
        saved = false;
        timer = Time.time; //Start the timer
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - timer > 300 && saved == false)
        {
            gameObject.GetComponent<Save>().save();
            saved = true;
        }
        if (Time.time - timer >= 340f) //After 5mins, boot the player back to the title screen
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("TitleScreen");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
