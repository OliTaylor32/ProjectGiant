using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public bool fishEating;
    private GameObject fishTarget;
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float turnspeed;

    public Transform[] targets;
    private int currentTarget;

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 forward = new Vector3(0, 0, 1);

    public bool fishGrabbed;
    public bool snowFish;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Object>().item == "bird")
        {
            targets = new Transform[GameObject.Find("BirdTargets").transform.childCount];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = GameObject.Find("BirdTargets").transform.GetChild(i);
            }
        }
        currentTarget = Random.Range(0, targets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (fishGrabbed == false)
        {
            if (fishEating == true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(fishTarget.transform.position.x, fishTarget.transform.position.y, fishTarget.transform.position.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targets[currentTarget].position.x, targets[currentTarget].position.y, targets[currentTarget].position.z) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
            }

            speed = speed + acceleration;

            speed = Mathf.Clamp(speed, 0, maxSpeed);

            transform.Translate(forward * speed * Time.deltaTime);
        }
        else
        {
            transform.position = targets[0].position;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            transform.localScale = transform.localScale * 0.995f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == targets[currentTarget].gameObject)
        {
            currentTarget = Random.Range(0, targets.Length);
            //print("New Target");
        }
        if (other.gameObject == fishTarget)
        {
            currentTarget = Random.Range(0, targets.Length);
            //print("New Target");
            fishEating = false;
            fishTarget.GetComponent<BirdAI>().FishGrabbed(gameObject);
            GameObject.Find("Giant").GetComponent<PlayerControl>().HoldingStolen();
            GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().FishTaken();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == fishTarget)
        {
            currentTarget = Random.Range(0, targets.Length);
            //print("New Target");
            fishEating = false;
            fishTarget.GetComponent<BirdAI>().FishGrabbed(gameObject);
            GameObject.Find("Giant").GetComponent<PlayerControl>().HoldingStolen();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject == targets[currentTarget].gameObject)
        {
            currentTarget = Random.Range(0, targets.Length);
        }
    }

    public void PickedUp()
    {
        print("Picked Up");
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().isTrigger = false;
        maxSpeed = 0.2f;
    }

    public void SetFishTarget(GameObject fish2Eat)
    {
        fishTarget = fish2Eat;
        GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().natureScore++;
    }

    public void FishGrabbed(GameObject bird)
    {
        Destroy(GetComponent<BoxCollider>());
        fishGrabbed = true;
        targets[0] = bird.transform;
        currentTarget = 0;
        StartBeingEaten();
    }

    private IEnumerator StartBeingEaten()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
