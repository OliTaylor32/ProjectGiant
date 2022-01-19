using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndStats : MonoBehaviour
{
    public GameObject text;
    private int dayVScore, dayNScore = 0;
    private int tPop, vPop, lPop, bPop = 0;

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
        dayVScore = villagerScore;
        dayNScore = natureScore;
        text.SetActive(true);
        text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Villager Score: " + dayVScore + Environment.NewLine
                                        + Environment.NewLine + "Nature Score: " + dayNScore + Environment.NewLine
                                        + Environment.NewLine + "Overall Score: " + (dayVScore + dayNScore) + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Current Populations: " + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Villager Population: " + vPop + Environment.NewLine
                                        + Environment.NewLine + "Tree Population: " + tPop + Environment.NewLine
                                        + Environment.NewLine + "Livestock Population: " + lPop + Environment.NewLine
                                        + Environment.NewLine + "Bird Population: " + bPop + Environment.NewLine;
    }

    public void SetPopulationStats(int villagers, int trees, int livestock, int birds)
    {
        vPop = villagers;
        tPop = trees;
        lPop = livestock;
        bPop = birds;
        text.SetActive(true);
        text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Villager Score: " + dayVScore + Environment.NewLine
                                        + Environment.NewLine + "Nature Score: " + dayNScore + Environment.NewLine
                                        + Environment.NewLine + "Overall Score: " + (dayVScore + dayNScore) + Environment.NewLine + Environment.NewLine
                                        + Environment.NewLine + "Villager Population: " + vPop + Environment.NewLine
                                        + Environment.NewLine + "Tree Population: " + tPop + Environment.NewLine
                                        + Environment.NewLine + "Livestock Population: " + lPop + Environment.NewLine
                                        + Environment.NewLine + "Bird Population: " + bPop + Environment.NewLine;
    }


}
