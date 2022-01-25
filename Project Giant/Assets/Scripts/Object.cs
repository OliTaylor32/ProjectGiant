using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string item;
    public int weight;
    public int life;
    public GameObject villager;
    public GameObject tree;
    public GameObject stats;

    private bool destructing;
    public GameObject smoke;

    public GameObject star;
    public GameObject tear;

    // Start is called before the first frame update
    void Start()
    {
        destructing = false;
        stats = GameObject.Find("Narrator");
        //if (item == "sapling") //If this object is a tree sapling, start growing
        //{
        //    StartCoroutine(Sapling());
        //}

        if (item == "sheep")
        {
            StartCoroutine(AudioPlay());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetWeight(GameObject sender) //Return this objects weight
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }


    public void lifeDown() //When damaged
    {
        life--;
        if (item == "sheep")
        {
            if (GetComponent<LiveStockAI>() != null)
            {
                GetComponent<LiveStockAI>().StopMoving();
            }
        }

        if (life < 1) //If it's depleated of all of its life, destory the game object
        {
            if (item != "sheep" && item != "fish")
            {
                if (item == "tree" || item == "treeWilt")
                {
                    stats.GetComponent<Dialogue>().natureScore--;
                }
                if (destructing == false)
                {
                    destructing = true;
                    StartCoroutine(Destruction());
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Built() 
    {
        if(item == "igloo") //Create 2 new villagers
        {
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5), Quaternion.identity);
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y + 0.5f, transform.position.z + 4), Quaternion.identity);
        }
    }

    //private IEnumerator Sapling() //Grow until full size, then replace with tree (that can reproduce)
    //{
    //    yield return new WaitForSeconds(120);    
    //    Destroy(gameObject.GetComponent<BoxCollider>());
    //    Instantiate(tree, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    //    Destroy(gameObject);


    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            if (item == "farm" || item == "scaffolding")
            {
                lifeDown();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            if (item == "farm" || item == "scaffolding")
            {
                lifeDown();
            }
        }
    }

    private IEnumerator Destruction()
    {
        GameObject smokeSpawned = null;
        GetComponent<BoxCollider>().isTrigger = true;
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (smoke != null)
        {
            smokeSpawned = Instantiate(smoke);
            smokeSpawned.transform.position = transform.position;
        }
        if (tear != null)
        {
            Instantiate(tear).transform.position = transform.position;
        }
        for (int i = 0; i < 500; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(Vector3.left * (5f * Time.deltaTime));
            transform.position += new Vector3(0f, -0.01f, 0f);
        }
        if (smokeSpawned != null)
        {
            smokeSpawned.GetComponent<ParticleSystem>().Stop();
        }
        Destroy(gameObject);
    }

    private IEnumerator AudioPlay()
    {
        while (life > 0)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
        }
    }
}
