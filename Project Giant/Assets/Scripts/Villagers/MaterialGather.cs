using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGather : MonoBehaviour
{
    //This script is used to check whether the vilager can build in this location.
    private GameObject villager;
    public GameObject tree;
    public int objects;
    private bool checking;
    private bool villageCheck;
    // Start is called before the first frame update
    void Start()
    {
        checking = false;
        //villageCheck = false;
        objects = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (checking == true)
        {
            if (objects == 0) //If there is nothing in the way, allow the villager to build.
            {
                villager.GetComponent<Villager>().canBuild = true;
                Destroy(gameObject);
            }

            if (Time.time - villager.GetComponent<Villager>().timer > 60) //If there is still something in the way after 60secs, stop.
            {
                Destroy(gameObject);
            }

            if (Time.time - villager.GetComponent<Villager>().timer == 5) //IF the villager can't build after 5 secs, tell the player about how they can help the villagers.
            {
                GameObject txtHint = GameObject.Find("Narrator");
                txtHint.SendMessage("BuildHelp", SendMessageOptions.DontRequireReceiver);
            }
        }


    }

    private void OnTriggerEnter(Collider collision)//An object is in the way, add one to amount of objects that need to be removed.
    {
        objects++;
    }

    private void OnTriggerExit(Collider other) // An object is no longer in the way, take one from the amount of objects that need to be moved.
    {
        objects--;
    }

    private void OnTriggerStay(Collider other)
    {
        if (villageCheck == true)
        {
            if (other.GetComponent<Villager>() != null)
            {
                if (villager != null)
                {
                    if (villager.GetComponent<Villager>().colour == "mRed" || villager.GetComponent<Villager>().colour == "mBlue" || villager.GetComponent<Villager>().colour == "mGreen")
                    {

                        if (other.GetComponent<Villager>().colour == "fRed" || other.GetComponent<Villager>().colour == "fBlue" || other.GetComponent<Villager>().colour == "fGreen")
                        {
                            villager.GetComponent<Villager>().BuildVillage();
                            other.GetComponent<Villager>().SetVillage();
                            Destroy(this);
                        }
                    }
                }
            }
        }
    }

    public void Check(GameObject sender) //Villager telling this object to start checking if its ok to build.
    {
        villager = sender;
        checking = true;
    }

    public void VillageBuild(GameObject sender)
    {
        villager = sender;
        villageCheck = true;
    }
    
}
