﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public string item;
    public int weight;
    public Transform townCenter;
    public int speed = 1;
    public int life;
    private string[] actions;

    private GameObject[] buildings;

    public GameObject snowMan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
        actions = new string[2];
        actions[0] = "Nothing";
        actions[1] = "Build";
        buildings = new GameObject[1];
        buildings[0] = snowMan;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Move()
    {
        Vector3 target = new Vector3((townCenter.position.x + Random.Range(-15, 15)), townCenter.position.y + 0.7f, (townCenter.position.z + Random.Range(-15, 15)));
        transform.LookAt(target);
        while (Vector3.Distance(transform.position, target) > 1)
        {
            //print("step");
            transform.position = Vector3.MoveTowards(transform.position, target, ((speed * 0.1f) * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(Action());

    }

    private void lifeDown()
    {
        life--;
    }

    private void GetWeight(GameObject sender)
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }

    private IEnumerator Action()
    {
        print("Action start");
        int action = Random.Range(0, 2);
        switch (actions[action])
        {
            case "Nothing":
                print("No Action Taken");
                break;
            case "Build":
                print("Building");
                action = /*Random.Range(0, 0); */ 0;
                Instantiate(buildings[action], new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                yield return new WaitForSeconds(5);
                break;
        }


        yield return new WaitForSeconds(3);
        print("Action end");
        StartCoroutine(Move());

    }

}
