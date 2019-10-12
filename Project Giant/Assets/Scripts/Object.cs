using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string item;
    public int weight;
    public Transform townCenter;
    public int speed = 1;
    public int life;

    // Start is called before the first frame update
    void Start()
    {
        if(item == "Villiger")
        {
            StartCoroutine(Move());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetWeight(GameObject sender)
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }

    private IEnumerator Move()
    {
        Vector3 target = new Vector3((townCenter.position.x + Random.Range(-5, 5)), townCenter.position.y, (townCenter.position.z + Random.Range(-5, 5)));
        transform.LookAt(target);
        while (Vector3.Distance(transform.position, target) > 1)
        {
            //print("step");
            transform.position = Vector3.MoveTowards(transform.position, target, ((speed * 0.1f) * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(Move());

    }
}
