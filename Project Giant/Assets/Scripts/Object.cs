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

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Narrator");
        if (item == "Sapling") //If this object is a tree sapling, start growing
        {
            StartCoroutine(Sapling());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0) //If it's depleated of all of its life, destory the game object
        {
            Destroy(gameObject);
        }
    }

    private void GetWeight(GameObject sender) //Return this objects weight
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }


    public void lifeDown() //When damaged
    {
        life--;
        if (item == "Tree")
        {
            stats.GetComponent<Dialogue>().natureScore--;
        }
    }

    private void Built() 
    {
        if(item == "smallHouse") //Create 2 new villagers
        {
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5), Quaternion.identity);
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y + 0.5f, transform.position.z + 4), Quaternion.identity);
        }
    }

    private IEnumerator Sapling() //Grow until full size, then replace with tree (that can reproduce)
    {
        yield return new WaitForSeconds(120);    
        Destroy(gameObject.GetComponent<BoxCollider>());
        Instantiate(tree, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);


    }
}
