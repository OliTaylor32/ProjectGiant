using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private AudioSource player;
    public AudioClip[] music;
    public AudioClip dayEndTheme;
    public AudioClip emergency;
    private bool endOfDay = false;
    private bool playEmergency;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<AudioSource>();

        player.clip = music[0];
        player.Play();
        StartCoroutine(Music());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Music()
    {
        yield return new WaitForSeconds(player.clip.length);
        yield return new WaitForSeconds(Random.Range(60f, 120f));
        while (endOfDay == false)
        {
            player.clip = music[Random.Range(1, music.Length)];
            player.Play();
            yield return new WaitForSeconds(player.clip.length);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
        //Play end of day music here
    }

    public void endOfDayAlert()
    {
        endOfDay = true;
        player.Pause();
        player.clip = dayEndTheme;
        player.Play();
    }

    private IEnumerator StartEmergency()
    {
        endOfDay = true;
        if (player.clip != emergency)
        {
            player.clip = emergency;
            player.Play();
            yield return new WaitForSeconds(player.clip.length);
            StartCoroutine(Music());
            yield return new WaitForSeconds(30f);
            endOfDay = false;
        }
    }
    public void Emergency()
    {
        StartCoroutine(StartEmergency());
    }
}
