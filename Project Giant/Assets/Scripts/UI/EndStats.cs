using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndStats : MonoBehaviour
{
    public GameObject text;
    public Transform camera;
    public Transform centerpoint;
    public bool challengeMode;
    private int dayVScore, dayNScore = 0;
    private int tPop, vPop, lPop, bPop, pPop = 0;

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
        if (challengeMode == false)
        {
            text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Villager Score: " + dayVScore + Environment.NewLine
                                            + Environment.NewLine + "Nature Score: " + dayNScore + Environment.NewLine
                                            + Environment.NewLine + "Overall Score: " + (dayVScore + dayNScore) + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Current Populations: " + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Villager Population: " + vPop + Environment.NewLine
                                            + Environment.NewLine + "Tree Population: " + tPop + Environment.NewLine
                                            + Environment.NewLine + "Livestock Population: " + lPop + Environment.NewLine
                                            + Environment.NewLine + "Bird Population: " + bPop + Environment.NewLine
                                            + Environment.NewLine + "Penguin Population: " + pPop + Environment.NewLine;
        }
        else
        {
            text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                + Environment.NewLine + "Villager Score (x5): " + dayVScore * 5 + Environment.NewLine
                                + Environment.NewLine + "Nature Score (x3): " + dayNScore * 3 + Environment.NewLine
                                + Environment.NewLine + "Villager Population (x3): " + vPop * 3 + Environment.NewLine
                                + Environment.NewLine + "Tree Population (x1): " + tPop + Environment.NewLine
                                + Environment.NewLine + "Livestock Population (x2): " + lPop * 2 + Environment.NewLine
                                + Environment.NewLine + "Bird Population (x2): " + bPop * 2 + Environment.NewLine
                                + Environment.NewLine + "Penguin Population (x2): " + pPop * 2 + Environment.NewLine
                                + Environment.NewLine + Environment.NewLine + "Final Score: " + ((dayVScore * 5) + (dayNScore * 3) + (vPop * 3) + tPop + (lPop * 2) + (bPop * 2) + (pPop * 2)) + Environment.NewLine;


        }

        centerpoint.localPosition = new Vector3(-16, 0, 5);
        centerpoint.gameObject.GetComponent<Animator>().enabled = true;
        camera.localPosition = new Vector3(0, 10, -73);
        camera.localEulerAngles = new Vector3(10, 0, 0); 
    }

    public void SetPopulationStats(int villagers, int trees, int livestock, int birds, int penguins)
    {
        vPop = villagers;
        tPop = trees;
        lPop = livestock;
        bPop = birds;
        pPop = penguins;
        text.SetActive(true);
        if (challengeMode == false)
        {
            text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Villager Score: " + dayVScore + Environment.NewLine
                                            + Environment.NewLine + "Nature Score: " + dayNScore + Environment.NewLine
                                            + Environment.NewLine + "Overall Score: " + (dayVScore + dayNScore) + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Current Populations: " + Environment.NewLine + Environment.NewLine
                                            + Environment.NewLine + "Villager Population: " + vPop + Environment.NewLine
                                            + Environment.NewLine + "Tree Population: " + tPop + Environment.NewLine
                                            + Environment.NewLine + "Livestock Population: " + lPop + Environment.NewLine
                                            + Environment.NewLine + "Bird Population: " + bPop + Environment.NewLine
                                            + Environment.NewLine + "Penguin Population: " + pPop + Environment.NewLine;
        }
        else
        {
            text.GetComponent<Text>().text = "End Of Day Stats:" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                                + Environment.NewLine + "Villager Score (x5): " + dayVScore * 5 + Environment.NewLine
                                + Environment.NewLine + "Nature Score (x3): " + dayNScore * 3 + Environment.NewLine
                                + Environment.NewLine + "Villager Population (x3): " + vPop * 3 + Environment.NewLine
                                + Environment.NewLine + "Tree Population (x1): " + tPop + Environment.NewLine
                                + Environment.NewLine + "Livestock Population (x2): " + lPop * 2 + Environment.NewLine
                                + Environment.NewLine + "Bird Population (x2): " + bPop * 2 + Environment.NewLine
                                + Environment.NewLine + "Penguin Population (x2): " + pPop * 2 + Environment.NewLine
                                + Environment.NewLine + Environment.NewLine + "Final Score: " + ((dayVScore * 5) + (dayNScore * 3) + (vPop * 3) + tPop + (lPop * 2) + (bPop * 2) + (pPop * 2)) + Environment.NewLine;


        }
    }


}
