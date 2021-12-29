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
    public int speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, -100f, 0f);
        stats = GameObject.Find("Narrator").GetComponent<Dialogue>();
        spawned = false;
        chance = 0;
        StartCoroutine(spawn());

        score = stats.natureScore;
        chanceCalc();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        if (spawned == false)
        {
            random = Random.Range(0, chance + 1);
            print(random);
            if (random == chance)
            {
                spawned = true;
                Vector3 start = new Vector3(Random.Range(-200, 200), 3, Random.Range(-200, 200));
                Vector3 end = new Vector3(Random.Range(-100, 100), 3, Random.Range(-100, 100));
                transform.position = start;
                //Become visable
                //Play audio
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
        if (score > -10)
        {
            chance = 5000;
        }

        if (score < -9)
        {
            chance = 4000;
        }

        if (score < -19)
        {
            chance = 3000;
        }

        if (score < -29)
        {
            chance = 3000;
        }

        if (score < -39)
        {
            chance = 2000;
        }

        if (score < -49)
        {
            chance = 1000;
        }

        if (score < -59)
        {
            chance = 600;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        collision.gameObject.SendMessage("lifeDown", SendMessageOptions.DontRequireReceiver);
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
