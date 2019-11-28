using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= 600)
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("TitleScreen");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
