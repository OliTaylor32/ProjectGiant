using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtractButton : MonoBehaviour
{
    public string type;
    public GameObject giant;
    private GameObject resources;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        if (type == "wood")
        {
            //if ()
            //{

            //}
        }
    }
}
