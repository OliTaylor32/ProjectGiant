using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTear : MonoBehaviour
{
    public bool isStar;
    public Transform giant;
    public Transform camera;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        giant = GameObject.Find("Giant").transform;
        camera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed + 0.05f;
        Vector3 target = new Vector3(giant.position.x, giant.position.y, giant.position.z);
        transform.LookAt(camera);
        transform.position = Vector3.MoveTowards(transform.position, target, (speed * Time.deltaTime));
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == giant.gameObject)
        {
            giant.gameObject.SendMessage("starTear", isStar, SendMessageOptions.DontRequireReceiver);
            Destroy(this);
        }
    }
}
