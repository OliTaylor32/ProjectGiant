﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public string function;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown() //Load the scene of the corresponding button
    {
        print("clicked");
        if (function == "newgame")
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("PlayArea");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        if (function == "quit")
        {
            Application.Quit();
        }
        if (function == "back")
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("TitleScreen");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        if (function == "help")
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("How To Play");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        if (function == "loadgame")
        {
            SceneManager.LoadScene("LoadData");
        }
    }
}
