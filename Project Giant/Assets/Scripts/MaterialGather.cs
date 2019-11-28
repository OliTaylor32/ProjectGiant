using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGather : MonoBehaviour
{
    private GameObject villager;
    public GameObject tree;
    public int objects;
    private bool checking;
    // Start is called before the first frame update
    void Start()
    {
        checking = false;
        objects = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (checking == true)
        {
            if (objects == 0)
            {
                villager.GetComponent<Villager>().canBuild = true;
                Destroy(gameObject);
            }

            if (Time.time - villager.GetComponent<Villager>().timer > 60)
            {
                Destroy(gameObject);
            }

            if (Time.time - villager.GetComponent<Villager>().timer == 5)
            {
                GameObject txtHint = GameObject.Find("Narrator");
                txtHint.SendMessage("BuildHelp", SendMessageOptions.DontRequireReceiver);
            }
        }


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

    public void Check(GameObject sender)
    {
        villager = sender;
        checking = true;
    }
}
