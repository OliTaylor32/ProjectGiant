using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeReproDetect : MonoBehaviour
{

    public GameObject tree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.SendMessage("GetType", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void GetType(GameObject sender)
    {
        sender.SendMessage("ReturnType", "tree", SendMessageOptions.DontRequireReceiver);
    }

    public void ReturnType(string type)
    {
        if (type == "tree")
        {
            reproduce();
        }
    }

    private void reproduce()
    {
        transform.parent.gameObject.SendMessage("lifeDown", SendMessageOptions.DontRequireReceiver);
        Instantiate(tree, new Vector3(transform.position.x + 5, transform.position.y + 5f, transform.position.z + 5), Quaternion.identity);
    }
}
