using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetect : MonoBehaviour
{

    public GameObject pickUp;
    private GameObject pickUpDummy;
    public GameObject giant;
    public GameObject fishEatingBird;
    // Start is called before the first frame update
    void Start()
    {
        pickUpDummy = GameObject.Find("Directional Light");
        pickUp = pickUpDummy;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUp == null)
        {
            pickUp = pickUpDummy;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (pickUp == null && collision.gameObject != giant && (collision.GetComponent<Object>() != null || collision.GetComponent<Villager>() != null))
        {
            pickUp = collision.transform.root.gameObject;
        }

    }

    public void GetPickUp() //return the giant the object that is to be picked up.
    {
        giant.SendMessage("ReturnPickUp", pickUp, SendMessageOptions.DontRequireReceiver);
        if (pickUp.GetComponent<Object>().item == "fish")
        {
            pickUp.GetComponent<BirdAI>().PickedUp();
            GameObject bird = Instantiate(fishEatingBird, new Vector3(transform.position.x + Random.Range(-70f, 70f), transform.position.y + 10f, transform.position.z + Random.Range(-70f, 70f)), Quaternion.identity);
            bird.GetComponent<BirdAI>().SetFishTarget(pickUp);
        }
    }

    public void AttackRequest() //Called by the giant
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack() //Once animation is finished, damage what was hit and allow giant to move again.
    {
        yield return new WaitForSeconds(1.7f);
        giant.SendMessage("AttackFinished", SendMessageOptions.DontRequireReceiver);
        print("Object Attacked");
        pickUp.SendMessage("lifeDown", SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerStay(Collider collision) //If it's the closest object to the giant inside the pick up zone, it will be the object to be picked up.
    {
        //print("Collision entered");
        if (((Vector3.Distance(collision.transform.position, giant.transform.position) < Vector3.Distance(pickUp.transform.position, giant.transform.position)) || pickUp == null) && collision.gameObject != giant && (collision.GetComponent<Object>() != null || collision.GetComponent<Villager>() != null))
        {
            pickUp = collision.transform.root.gameObject;
        }
    }

    public void OnTriggerExit(Collider collision) //If there is nothing left to be picked up, pick up object that doesn't matter as to not result in a null error
    {
        if (collision.gameObject == pickUp)
        {
            pickUp = pickUpDummy;
        }

    }
}
