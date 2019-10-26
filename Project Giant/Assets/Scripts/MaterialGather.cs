using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGather : MonoBehaviour
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Object>() != null)
        {
            if (collision.gameObject.GetComponent<Object>().item == "Tree")
               tree = collision.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (tree == other.gameObject)
        {
            tree = null;
        }
    }
}
