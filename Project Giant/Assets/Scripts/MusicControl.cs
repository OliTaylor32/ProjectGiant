using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private AudioSource player;
    public AudioClip[] music;
    public AudioClip dayEndTheme;
    private bool endOfDay = false;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<AudioSource>();

        player.clip = music[0];
        player.Play();
        StartCoroutine(Music());
        print(music.Length);
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
        player.clip = dayEndTheme;
        player.Play();
    }
}
