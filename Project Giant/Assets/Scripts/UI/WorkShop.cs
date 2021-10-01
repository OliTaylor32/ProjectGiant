using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkShop : MonoBehaviour
{
    public GameObject menuBG;
    public GameObject selection1;
    public GameObject selection2;
    public GameObject selection3;
    public GameObject selection4;
    public bool open;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        menuBG = GameObject.Find("MenuBG");
        menuBG.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z") && open == true)
        {
            CloseWorkShop();
        }
    }

    public void OpenWorkShop()
    {
        print("Opened");
        menuBG.SetActive(true);
        menuBG.GetComponent<Animator>().Play("MenuBGIn");
        StartCoroutine(Waiting());
    }

    public void CloseWorkShop()
    {
        print("Closed");
        GameObject.Find("Giant").GetComponent<PlayerControl>().WorkShopExit();
        menuBG.GetComponent<Animator>().Play("MenuBGOut");
        StartCoroutine(Waiting());

    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        if (open == false)
        {
            open = true;
        }
        else if (open == true)
        {
            open = false;
            menuBG.SetActive(false);
        }
    }
}
