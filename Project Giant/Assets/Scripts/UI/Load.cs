using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    SaveData data;
    private float[,] trees;
    private float[,] igloos;
    private float[,] bWW;
    private float[,] snowMen;
    private float[,] torches;
    private float[,] totems;

    private int blueVillagers;
    private int blackVillagers;

    public GameObject tree;
    public GameObject igloo;
    public GameObject basicWoodWorkshop;
    public GameObject snowMan;
    public GameObject torch;
    public GameObject totem;
    public GameObject blueVillager;
    public GameObject blackVillager;

    public Transform blueCentre;
    public Transform blackCentre;

    // Start is called before the first frame update
    void Start()
    {
        data = new SaveData();
        data = data.Load();

        trees = data.trees;
        igloos = data.igloos;
        bWW = data.bWoodWorkshops;
        snowMen = data.snowMen;
        torches = data.torches;
        totems = data.totems;

        blueVillagers = data.blueVillagers;
        blackVillagers = data.blackVillagers;

        if (trees != null)
        {
            for (int i = 0; i < trees.GetLength(0); i++)
            {
                GameObject obj = Instantiate(tree, new Vector3(trees[i, 0], 20, trees[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, trees[i, 2], 0f);
            }
        }

        if (igloos != null)
        {
            for (int i = 0; i < igloos.GetLength(0); i++)
            {
                GameObject obj = Instantiate(igloo, new Vector3(igloos[i, 0], 20, igloos[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, igloos[i, 2], 0f);
            }
        }

        if (bWW != null)
        {

            for (int i = 0; i < bWW.GetLength(0); i++)
            {
                GameObject obj = Instantiate(basicWoodWorkshop, new Vector3(bWW[i, 0], 20, bWW[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, bWW[i, 2], 0f);
            }
        }

        if (snowMen != null)
        {
            for (int i = 0; i < snowMen.GetLength(0); i++)
            {
                GameObject obj = Instantiate(snowMan, new Vector3(snowMen[i, 0], 20, snowMen[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, snowMen[i, 2], 0f);
            }
        }

        if (torches != null)
        {
            for (int i = 0; i < torches.GetLength(0); i++)
            {
                GameObject obj = Instantiate(torch, new Vector3(torches[i, 0], 20, torches[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, torches[i, 2], 0f);
            }
        }

        if (totems != null)
        {
            for (int i = 0; i < totems.GetLength(0); i++)
            {
                GameObject obj = Instantiate(totem, new Vector3(totems[i, 0], 20, totems[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles.Set(0f, totems[i, 2], 0f);
            }
        }

        for (int i = 0; i < blueVillagers; i++)
        {
            Instantiate(blueVillager, new Vector3(Random.Range(blueCentre.position.x - 20.0f, blueCentre.position.x + 20.0f), 20, Random.Range(blueCentre.position.z - 20.0f, blueCentre.position.z + 20.0f)), Quaternion.identity);
        }

        for (int i = 0; i < blackVillagers; i++)
        {
            Instantiate(blackVillager, new Vector3(Random.Range(blackCentre.position.x - 20.0f, blackCentre.position.x + 20.0f), 20, Random.Range(blackCentre.position.z - 20.0f, blackCentre.position.z + 20.0f)), Quaternion.identity);
        }

    }

}
