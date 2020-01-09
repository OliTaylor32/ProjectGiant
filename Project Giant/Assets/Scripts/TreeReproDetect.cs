﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeReproDetect : MonoBehaviour
{

    public GameObject tree;
    public GameObject giant;
    public GameObject currentCollision;
    private bool checking;
    public GameObject stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Narrator");
        giant = GameObject.Find("Giant");
        checking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        currentCollision = other.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (checking == false)
        {
            checking = true;
            StartCoroutine(TriggerEnter(other)); //Start cheching if it's another tree
        }
 
    }

    public IEnumerator TriggerEnter(Collider other)
    {
        if (other.transform.root != other.transform) //If this object is a child.
        {
            //Wait until neither this tree or the other tree is being carried by the giant
            yield return new WaitUntil(() => giant.GetComponent<PlayerControl>().carrying != gameObject.transform.parent.gameObject 
                                                && giant.GetComponent<PlayerControl>().carrying != other.gameObject.transform.parent.gameObject);
            if (other.gameObject == currentCollision) //If it's the same object
            {
                if (other.transform.parent.GetComponent<Object>() != null) //If it is an object
                {
                    if (other.transform.parent.GetComponent<Object>().item == "Tree") //If it's a tree
                    {
                        StartCoroutine(reproduce()); //Reproduce
                    }

                }
            }
        }
        checking = false;

    }

    private IEnumerator reproduce() //Create 2 new saplings and die
    {
        stats.GetComponent<Dialogue>().natureScore++;
        Instantiate(tree, new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + 5, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        Instantiate(tree, new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + 5, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        transform.parent.gameObject.GetComponent<Animator>().Play("TreeWilt");
        yield return new WaitForSeconds(5);
        Destroy(transform.parent.gameObject);
        
    }
}
