using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float turnspeed;

    public Transform[] targets;
    private int currentTarget;

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 forward = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = Random.Range(0, targets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targets[currentTarget].position.x, targets[currentTarget].position.y, targets[currentTarget].position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        speed = speed + acceleration;

        speed = Mathf.Clamp(speed, 5, maxSpeed);

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
}
