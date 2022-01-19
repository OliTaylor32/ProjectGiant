using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveStockAI : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float turnspeed;

    public Transform[] targets;
    private int currentTarget;

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 forward = new Vector3(0, 0, 1);

    public bool canBreed;
    public GameObject child;



    // Start is called before the first frame update
    void Start()
    {
         GameObject getTargets = GameObject.Find("LiveStockTargets");
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
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targets[currentTarget].position.x, transform.position.y, targets[currentTarget].position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        speed = speed + acceleration;

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.Translate(forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == targets[currentTarget].gameObject)
        {
            currentTarget = Random.Range(0, targets.Length);
            print("New Target");
        }
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
        if (collision.gameObject.GetComponent<LiveStockAI>() != null)
        {
            if (canBreed == true)
            {
                GameObject newChild = Instantiate(child, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z + 0.5f), Quaternion.identity);
                canBreed = false;
                newChild.GetComponent<LiveStockAI>().SetTarget(currentTarget);
            }
        }
    }

    public void StopMoving()
    {
        GetComponent<Animator>().Play("SheepDead");
        Destroy(this);
    }

    public void SetTarget(int newTarget)
    {
        currentTarget = newTarget;
    }
}
