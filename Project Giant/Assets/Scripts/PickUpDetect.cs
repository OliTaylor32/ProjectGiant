using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetect : MonoBehaviour
{

    public GameObject pickUp;
    public GameObject giant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        //print("Collision entered");
        pickUp = collision.gameObject;
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
}
