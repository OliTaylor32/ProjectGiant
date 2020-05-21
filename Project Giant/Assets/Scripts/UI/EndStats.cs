using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndStats : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats(int villagerScore, int natureScore)
    {
        text.SetActive(true);
        text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Villager Score:" + villagerScore + Environment.NewLine
                                        + Environment.NewLine + "Nature Score:" + natureScore + Environment.NewLine
                                        + Environment.NewLine + "Overall Score:" + (villagerScore + natureScore); 
    }
}
