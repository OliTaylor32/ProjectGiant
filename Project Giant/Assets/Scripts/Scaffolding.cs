using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffolding : MonoBehaviour
{
    public GameObject building;
    public GameObject villager;
    public int newVillagers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBuild());
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, Random.Range(-180, 180), transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartBuild()
    {
        yield return new WaitForSeconds(60f);
        GameObject newBuild = Instantiate(building, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        newBuild.transform.rotation = gameObject.transform.rotation;
        yield return new WaitForSeconds(45f);
        for (int i = 0; i < newVillagers; i++)
        {
            Instantiate(villager, new Vector3(transform.position.x + 0.5f, transform.position.y + 1, transform.position.z + (i / 5)), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
