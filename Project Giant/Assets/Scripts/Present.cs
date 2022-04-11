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
            print("Is player");
            Instantiate(objects[item], transform.position, Quaternion.identity);
            print("Item Spawned");
            Instantiate(particles, transform.position, Quaternion.Euler(-90, 0, 0));
            print("Particles Spawned");
            StartCoroutine(WaitforSpawn());
        }
    }

    private IEnumerator WaitforSpawn()
    {
        print("Wait for spawn");
        item = Random.Range(0, objects.Length);
        print("New Item Decided");
        GetComponent<Rigidbody>().useGravity = false;
        print("Use gravity = false");
        transform.position = new Vector3(Random.Range(-20, 20), 100, Random.Range(-20, 20));
        print("Start Wait");
        yield return new WaitForSeconds(Random.Range(0, 180));
        GetComponent<Rigidbody>().useGravity = true;
    }
}
