using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public GameObject[] objects;
    private int item;
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitforSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("Collision");
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            Instantiate(objects[item], transform.position, Quaternion.identity);
            Instantiate(particles, transform.position, Quaternion.Euler(-90, 0, 0));
            StartCoroutine(WaitforSpawn());
        }
    }

    private IEnumerator WaitforSpawn()
    {
        item = Random.Range(0, objects.Length);
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = new Vector3(Random.Range(-40, 40), 100, Random.Range(-40, 40));
        yield return new WaitForSeconds(Random.Range(0, 180));
        GetComponent<Rigidbody>().useGravity = true;
    }
}
