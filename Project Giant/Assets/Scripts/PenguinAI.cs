using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAI : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float turnspeed;

    public Transform[] targets;
    private int currentTarget;

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 forward = new Vector3(0, 0, 1);

    public bool fish;
    public bool fishGrabber;
    private GameObject fishTarget;
    public GameObject fishProp;

    // Start is called before the first frame update
    void Start()
    {
        fish = false;
        fishProp.SetActive(false);
        GameObject getTargets = GameObject.Find("PenguinTargets");
        targets = new Transform[getTargets.transform.childCount];
        for (int i = 0; i < getTargets.transform.childCount; i++)
        {
            targets[i] = getTargets.transform.GetChild(i);
        }
        currentTarget = Random.Range(0, targets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (fishGrabber == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targets[currentTarget].position.x, transform.position.y, targets[currentTarget].position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
        }
        else
        {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(fishTarget.transform.position.x, fishTarget.transform.position.y, fishTarget.transform.position.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
                GetComponent<Animator>().Play("PenguinSlide");
                if (Vector3.Distance(transform.position, fishTarget.transform.position) < 5)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                }
                maxSpeed = 2f;
                turnspeed = Vector3.Distance(transform.position, fishTarget.transform.position) * 0.5f;
        }

        speed = speed + acceleration;

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.Translate(forward * speed * Time.deltaTime);
    }

    //If it's in water, slide and go 3x speed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Water")
        {
            maxSpeed = maxSpeed * 5f;
            GetComponent<Animator>().Play("PenguinSlide");
        }
    }

    //when exiting water, have fish in mouth
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Water")
        {
            maxSpeed = maxSpeed / 5f;
            GetComponent<Animator>().Play("PenguinWalk");
            fish = true;
            fishProp.SetActive(true);
        }
    }

    public void SetFishTarget(GameObject fish2Eat)
    {
        fishTarget = fish2Eat;
        //transform.position = fishTarget.transform.position;
        GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().natureScore++;
    }

    public void SetTarget(int newTarget)
    {
        currentTarget = newTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject == targets[currentTarget].gameObject)
        {
            currentTarget = Random.Range(0, targets.Length);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == fishTarget)
        {
            currentTarget = Random.Range(0, targets.Length);
            //print("New Target");
            fishGrabber = false;
            fishTarget.GetComponent<BirdAI>().FishGrabbed(gameObject);
            GameObject.Find("Giant").GetComponent<PlayerControl>().HoldingStolen();
            GetComponent<Animator>().Play("PenguinWalk");
            GetComponent<Rigidbody>().useGravity = true;
            maxSpeed = 0.1f;
            turnspeed = 1f;
        }
    }

    public void FishTaken()
    {
        currentTarget = Random.Range(0, targets.Length);
        fish = false;
        fishProp.SetActive(false);
    }

}
