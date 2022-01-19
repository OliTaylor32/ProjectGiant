using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    SaveData data;
    private float[,] trees;
    private float[,] treeWilts;
    private float[,] stones;
    private float[,] redSmallHouses;
    private float[,] redFarms;
    private float[,] torches;
    private float[,] totems;

    private int mRedVillagers;
    private int fRedVillagers;

    private int livestock;
    private int birds;

    public GameObject tree;
    public GameObject treeWilt;
    public GameObject stone;
    public GameObject redSmallHouse;
    public GameObject redFarm;
    public GameObject torch;
    public GameObject totem;
    public GameObject mRedVillager;
    public GameObject fRedVillager;
    public GameObject livestockObj;
    public GameObject bird;


    public Transform redCentre;
    public Transform blackCentre;

    // Start is called before the first frame update
    void Start()
    {
        data = new SaveData();
        data = data.Load();

        trees = data.trees;
        treeWilts = data.treeWilts;
        stones = data.stones;
        redSmallHouses = data.redSmallHouses;
        redFarms = data.redFarms;
        torches = data.torches;
        totems = data.totems;

        mRedVillagers = data.mRedVillagers;
        fRedVillagers = data.fRedVillagers;

        livestock = data.livestock;
        birds = data.bird;
        

        if (trees != null)
        {
            for (int i = 0; i < trees.GetLength(0); i++)
            {
                GameObject obj = Instantiate(tree, new Vector3(trees[i, 0], 5, trees[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, trees[i, 2], 0f);
            }
        }

        if (treeWilts != null)
        {
            for (int i = 0; i < treeWilts.GetLength(0); i++)
            {
                GameObject obj = Instantiate(treeWilt, new Vector3(treeWilts[i, 0], 5, treeWilts[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, treeWilts[i, 2], 0f);
            }
        }

        if (stones != null)
        {
            for (int i = 0; i < stones.GetLength(0); i++)
            {
                GameObject obj = Instantiate(stone, new Vector3(stones[i, 0], 5, stones[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, stones[i, 2], 0f);
            }
        }

        if (redSmallHouses != null)
        {

            for (int i = 0; i < redSmallHouses.GetLength(0); i++)
            {
                GameObject obj = Instantiate(redSmallHouse, new Vector3(redSmallHouses[i, 0], 5, redSmallHouses[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, redSmallHouses[i, 2], 0f);
            }
        }

        if (redFarms != null)
        {
            for (int i = 0; i < redFarms.GetLength(0); i++)
            {
                GameObject obj = Instantiate(redFarm, new Vector3(redFarms[i, 0], 5, redFarms[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, redFarms[i, 2], 0f);
            }
        }

        if (torches != null)
        {
            for (int i = 0; i < torches.GetLength(0); i++)
            {
                GameObject obj = Instantiate(torch, new Vector3(torches[i, 0], 5, torches[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, torches[i, 2], 0f);
            }
        }

        if (totems != null)
        {
            for (int i = 0; i < totems.GetLength(0); i++)
            {
                GameObject obj = Instantiate(totem, new Vector3(totems[i, 0], 5, totems[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, totems[i, 2], 0f);
            }
        }

        for (int i = 0; i < mRedVillagers; i++)
        {
            Instantiate(mRedVillager, new Vector3(Random.Range(redCentre.position.x - 20.0f, redCentre.position.x + 20.0f), 5, Random.Range(redCentre.position.z - 20.0f, redCentre.position.z + 20.0f)), Quaternion.identity);
        }

        for (int i = 0; i < fRedVillagers; i++)
        {
            Instantiate(fRedVillager, new Vector3(Random.Range(redCentre.position.x - 20.0f, redCentre.position.x + 20.0f), 5, Random.Range(redCentre.position.z - 20.0f, redCentre.position.z + 20.0f)), Quaternion.identity);
        }

        for (int i = 0; i < livestock; i++)
        {
            Instantiate(livestockObj, new Vector3(Random.Range(redCentre.position.x - 20.0f, redCentre.position.x + 20.0f), 5, Random.Range(redCentre.position.z - 20.0f, redCentre.position.z + 20.0f)), Quaternion.identity);
        }

        for (int i = 0; i < birds; i++)
        {
            Instantiate(bird, new Vector3(Random.Range(redCentre.position.x - 20.0f, redCentre.position.x + 20.0f), 5, Random.Range(redCentre.position.z - 20.0f, redCentre.position.z + 20.0f)), Quaternion.identity);
        }

    }

}
