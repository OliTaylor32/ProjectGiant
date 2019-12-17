using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeReproDetect : MonoBehaviour
{

    public GameObject tree;
    public GameObject giant;
    public GameObject currentCollision;
    private bool checking;
    // Start is called before the first frame update
    void Start()
    {
        giant = GameObject.Find("Giant");
        checking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        currentCollision = other.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (checking == false)
        {
            checking = true;
            StartCoroutine(TriggerEnter(other));
        }
 
    }

    public IEnumerator TriggerEnter(Collider other)
    {
        if (other.transform.root != other.transform)
        {
            yield return new WaitUntil(() => giant.GetComponent<PlayerControl>().carrying != gameObject.transform.parent.gameObject 
                                                && giant.GetComponent<PlayerControl>().carrying != other.gameObject.transform.parent.gameObject);
            print("No longer carrying");
            if (other.gameObject == currentCollision)
            {
                print("Is same collision");
                if (other.transform.parent.GetComponent<Object>() != null)
                {
                    print("Is object");
                    if (other.transform.parent.GetComponent<Object>().item == "Tree")
                    {
                        print("Is tree");
                        StartCoroutine(reproduce());
                    }

                }
                //print("ask if tree");
                //other.SendMessage("GetType", gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
        checking = false;

    }

    //public void GetType(GameObject sender)
    //{
    //    print("Yes tree");
    //    sender.SendMessage("ReturnType", "tree", SendMessageOptions.DontRequireReceiver);
    //}

    //public void ReturnType(string type)
    //{
    //    if (type == "tree")
    //    {
    //        print("Create Tree");
    //        reproduce();
    //    }
    //}

    private IEnumerator reproduce()
    {
        Instantiate(tree, new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + 5, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        Instantiate(tree, new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + 5, transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
        print("Sapling Created");
        transform.parent.gameObject.GetComponent<Animator>().Play("TreeWilt");
        yield return new WaitForSeconds(5);
        Destroy(transform.parent.gameObject);
        //transform.parent.gameObject.SendMessage("lifeDown", SendMessageOptions.DontRequireReceiver);
        
    }
}
