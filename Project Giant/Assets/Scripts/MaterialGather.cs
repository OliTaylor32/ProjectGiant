using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGather : MonoBehaviour
{
    private GameObject villager;
    public GameObject tree;
    public int objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Object>() != null)
        {
            if (collision.gameObject.GetComponent<Object>().item == "Tree")
               tree = collision.gameObject;
        }
        objects++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (tree == other.gameObject)
        {
            tree = null;
        }
        objects--;

    }

    private void Check(GameObject sender)
    {
        villager = sender;
        while(Time.time - villager.GetComponent<Villager>().timer > 60)
        {
            if (objects == 0)
            {
                villager.GetComponent<Villager>().canBuild = true;
                Destroy(gameObject);
            }

            if (Time.time - villager.GetComponent<Villager>().timer > 59)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
