using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Dialogue stats;
    public int score;
    public bool spawned;
    public int chance;
    private int random;
    public int speed;
    private MusicControl music;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -100f, 0f);
        stats = GameObject.Find("Narrator").GetComponent<Dialogue>();
        music = GameObject.Find("Main Camera").GetComponent<MusicControl>();
        spawned = false;
        chance = 0;
        StartCoroutine(spawn());

        chanceCalc();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        chanceCalc();
        if (spawned == false)
        {
            random = Random.Range(0, chance + 1);
            if (random == chance)
            {
                spawned = true;
                print("Tornado");
                Vector3 start = new Vector3(Random.Range(-200, 200), 3.6f, Random.Range(-200, 200));
                Vector3 end = new Vector3(Random.Range(-100, 100), 3.6f, Random.Range(-100, 100));
                transform.position = start;
                //Become visable
                music.Emergency();
                //Start animation 
                while (Vector3.Distance(transform.position, end) > 1)
                {

                    transform.position = Vector3.MoveTowards(transform.position, end, (speed * Time.deltaTime));
                    yield return new WaitForSeconds(0.01f);
                }
            }

        }
        transform.position = new Vector3(0f, -100f, 0f);
        StartCoroutine(spawn());
    }

    private void chanceCalc()
    {
        score = stats.natureScore;
        if (score > -10)
        {
            chance = 2500;
        }

        if (score < -9)
        {
            chance = 2000;
        }

        if (score < -19)
        {
            chance = 1500;
        }

        if (score < -29)
        {
            chance = 1000;
        }

        if (score < -39)
        {
            chance = 500;
        }

        if (score < -49)
        {
            chance = 250;
        }

        if (score < -59)
        {
            chance = 100;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Object>() != null)
        {
            collision.gameObject.GetComponent<Object>().lifeDown();
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Object>() != null)
        {
            collision.gameObject.GetComponent<Object>().lifeDown();
        }
        else if (collision.gameObject.GetComponent<Villager>() != null)
        {
            collision.gameObject.GetComponent<Villager>().lifeDown();
        }
    }


}
