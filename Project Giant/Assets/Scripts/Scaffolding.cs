using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffolding : MonoBehaviour
{
    public GameObject building;
    public GameObject villager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBuild());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(60f);
        Instantiate(building, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(45f);
        Instantiate(villager, new Vector3(transform.position.x + 0.5f, transform.position.y + 1, transform.position.z), Quaternion.identity);
        Instantiate(villager, new Vector3(transform.position.x - 0.5f, transform.position.y + 1, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
