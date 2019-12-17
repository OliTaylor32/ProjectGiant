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

    // Start is called before the first frame update
    void Start()
    {
        if (item == "Sapling")
        {
            StartCoroutine(Sapling());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetWeight(GameObject sender)
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }


    public void lifeDown()
    {
        life--;
    }

    private void Built()
    {
        if(item == "smallHouse")
        {
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5), Quaternion.identity);
            Instantiate(villager, new Vector3(transform.position.x + 5, transform.position.y + 0.5f, transform.position.z + 4), Quaternion.identity);
        }
    }

    private IEnumerator Sapling()
    {
        yield return new WaitForSeconds(120);    
        Destroy(gameObject.GetComponent<BoxCollider>());
        Instantiate(tree, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);


    }
}
