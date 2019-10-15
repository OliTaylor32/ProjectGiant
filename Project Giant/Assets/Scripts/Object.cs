﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string item;
    public int weight;
    public int life;
    public GameObject villager;

    // Start is called before the first frame update
    void Start()
    {
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


    private void lifeDown()
    {
        life--;
    }

    private void Built()
    {
        if(item == "smallHouse")
        {
            Instantiate(villager, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2), Quaternion.identity);
            Instantiate(villager, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z + 2), Quaternion.identity);
        }
    }
}
