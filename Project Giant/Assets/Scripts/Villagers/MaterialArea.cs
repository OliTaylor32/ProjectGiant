using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialArea : MonoBehaviour
{
    public Transform tree;
    public Transform stone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Object>() != null)
        {
            switch (other.gameObject.GetComponent<Object>().item)
            {
                case "tree":
                    if (tree == null)
                    {
                        tree = other.transform;
                    }
                    break;

                case "stone":
                    if (stone == null)
                    {
                        stone = other.transform;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
