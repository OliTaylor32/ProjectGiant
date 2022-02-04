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
    public GameObject particleFX;

    private bool saplings;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Canvas").transform.Find("Narrator").gameObject;
        giant = GameObject.Find("Giant");
        checking = false;
        saplings = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentCollision)
        {
            currentCollision = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (checking == false && other.transform.root.GetComponent<PlayerControl>() == null)
        {
            checking = true;
            currentCollision = other.gameObject;
            StartCoroutine(TriggerEnter(other)); //Start cheching if it's another tree
        }
 
    }

    public IEnumerator TriggerEnter(Collider other)
    {
        if (other.transform.root != other.transform) //If this object is a child.
        {
            print("Object is Child");
            //Wait until neither this tree or the other tree is being carried by the giant
            yield return new WaitUntil(() => giant.GetComponent<PlayerControl>().carrying != gameObject.transform.parent.gameObject 
                                                && giant.GetComponent<PlayerControl>().carrying != other.gameObject.transform.parent.gameObject);
            print("Object is dropped");
            if (other.gameObject == currentCollision) //If it's the same object
            {
                print("Still Colliding with other object");
                if (other.transform.parent.GetComponent<Object>() != null) //If it is an object
                {
                    print("Is an object");
                    if (other.transform.parent.GetComponent<Object>().item == "tree" || other.transform.parent.GetComponent<Object>().item == "treeWilt") //If it's a tree
                    {
                        print("Is a tree");
                        StartCoroutine(reproduce()); //Reproduce
                    }

                }
            }
        }
        checking = false;

    }

    private IEnumerator reproduce() //Create 2 new saplings and die
    {
        if (saplings == false)
        {
            saplings = true;

            stats.GetComponent<Dialogue>().natureScore++;
            GameObject particles = Instantiate(particleFX, transform.position, Quaternion.Euler(-90, 0, 0));
            Instantiate(tree, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + 3, transform.position.z + Random.Range(-5, 5)), Quaternion.identity);
            Instantiate(tree, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + 5, transform.position.z + Random.Range(-5, 5)), Quaternion.identity);
            transform.parent.gameObject.GetComponent<Animator>().Play("TreeWilt");
            yield return new WaitForSeconds(5);
            Destroy(particles);
            Destroy(transform.parent.gameObject);
        }
    }
}
