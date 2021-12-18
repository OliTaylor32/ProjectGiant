using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBox : MonoBehaviour
{
    private Transform camera;
    public Sprite[] callTypes;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera);
    }

    public void setType(int type)
    {
        GetComponent<SpriteRenderer>().sprite = callTypes[type];   
    }
}
