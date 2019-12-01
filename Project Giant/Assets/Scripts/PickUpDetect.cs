using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetect : MonoBehaviour
{

    public GameObject pickUp;
    private GameObject pickUpDummy;
    public GameObject giant;
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

    public void GetPickUp()
    {
        //print("GetPickUp recieved");
        giant.SendMessage("ReturnPickUp", pickUp, SendMessageOptions.DontRequireReceiver);
        //print("Returned pickup");
    }

    public void AttackRequest()
    {
        StartCoroutine(Attack());

    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.7f);
        giant.SendMessage("AttackFinished", SendMessageOptions.DontRequireReceiver);
        print("Object Attacked");
        pickUp.SendMessage("lifeDown", SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerStay(Collider collision)
    {
        //print("Collision entered");
        if (((Vector3.Distance(collision.transform.position, giant.transform.position) < Vector3.Distance(pickUp.transform.position, giant.transform.position)) || pickUp == null) && collision.gameObject != giant && (collision.GetComponent<Object>() != null || collision.GetComponent<Villager>() != null))
        {

            pickUp = collision.transform.root.gameObject;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == pickUp)
        {
            pickUp = pickUpDummy;
        }

    }
}
