using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    SaveData data;
    private float[,] trees;
    private float[,] treeWilts;
    private float[,] snowTrees;
    private float[,] snowTreeWilts;
    private float[,] stones;
    private float[,] redSmallHouses;
    private float[,] blueSmallHouses;
    private float[,] redFarms;
    private float[,] torches;
    private float[,] totems;
    private float[,] villages;

    private int livestock;
    private int birds;
    private int penguins;
    public int nature;

    public GameObject tree;
    public GameObject treeWilt;
    public GameObject snowTree;
    public GameObject snowTreeWilt;
    public GameObject stone;
    public GameObject[] redSmallHouse;
    public GameObject[] blueSmallHouse;
    public GameObject redFarm;
    public GameObject torch;
    public GameObject totem;
    public GameObject townCenter;
    public GameObject livestockObj;
    public GameObject bird;
    public GameObject penguin;

    public GameObject[] mRedVillager;
    public GameObject[] fRedVillager;
    public GameObject[] mBlueVillager;
    public GameObject[] fBlueVillager;
    public GameObject[] mGreenVillager;
    public GameObject[] fGreenVillager;

    public string map;

    // Start is called before the first frame update
    void Start()
    {
        data = new SaveData();

        data = data.Load(map);

        trees = data.trees;
        treeWilts = data.treeWilts;
        snowTrees = data.snowTrees;
        snowTreeWilts = data.snowTreeWilts;
        stones = data.stones;
        redSmallHouses = data.redSmallHouses;
        blueSmallHouses = data.blueSmallHouses;
        redFarms = data.redFarms;
        torches = data.torches;
        totems = data.totems;

        villages = data.villages;

        livestock = data.livestock;
        birds = data.bird;
        penguins = data.penguins;
        nature = data.nature;
        
        if (trees != null)
        {
            for (int i = 0; i < trees.GetLength(0); i++)
            {
                GameObject obj = Instantiate(tree, new Vector3(trees[i, 0], 5, trees[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, trees[i, 2], 0f);
            }
        }
        print("TreeWilts check");

        if (treeWilts != null)
        { 
            for (int i = 0; i < treeWilts.GetLength(0); i++)
            {
                GameObject obj = Instantiate(treeWilt, new Vector3(treeWilts[i, 0], 5, treeWilts[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, treeWilts[i, 2], 0f);
            }
        }

        print("TreeWilts check end");

        if (snowTrees != null)
        {
            for (int i = 0; i < snowTrees.GetLength(0); i++)
            {
                GameObject obj = Instantiate(snowTree, new Vector3(snowTrees[i, 0], 5, snowTrees[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, snowTrees[i, 2], 0f);
            }
        }
        print("snowTreeWilts check");

        if (snowTreeWilts != null)
        {
            for (int i = 0; i < snowTreeWilts.GetLength(0); i++)
            {
                GameObject obj = Instantiate(snowTreeWilt, new Vector3(snowTreeWilts[i, 0], 5, snowTreeWilts[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, snowTreeWilts[i, 2], 0f);
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
                GameObject obj = Instantiate(redSmallHouse[Mathf.RoundToInt(redSmallHouses[i, 3])], new Vector3(redSmallHouses[i, 0], 5, redSmallHouses[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, redSmallHouses[i, 2], 0f);
            }
        }

        if (blueSmallHouses != null)
        {

            for (int i = 0; i < blueSmallHouses.GetLength(0); i++)
            {
                GameObject obj = Instantiate(blueSmallHouse[Mathf.RoundToInt(blueSmallHouses[i, 3])], new Vector3(blueSmallHouses[i, 0], 5, blueSmallHouses[i, 1]), Quaternion.identity);
                obj.transform.eulerAngles = new Vector3(0f, blueSmallHouses[i, 2], 0f);
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

        if (villages != null)
        {
            for (int i = 0; i < villages.GetLength(0); i++)
            {
                GameObject obj = Instantiate(townCenter, new Vector3(villages[i, 0], villages[i, 1], villages[i, 2]), Quaternion.identity);
                obj.GetComponent<TownCentre>().redM = Mathf.RoundToInt(villages[i, 3]);
                obj.GetComponent<TownCentre>().redF = Mathf.RoundToInt(villages[i, 4]);
                obj.GetComponent<TownCentre>().blueM = Mathf.RoundToInt(villages[i, 5]);
                obj.GetComponent<TownCentre>().blueF = Mathf.RoundToInt(villages[i, 6]);
                obj.GetComponent<TownCentre>().greenM = Mathf.RoundToInt(villages[i, 7]);
                obj.GetComponent<TownCentre>().greenF = Mathf.RoundToInt(villages[i, 8]);
                obj.GetComponent<TownCentre>().colour = Mathf.RoundToInt(villages[i, 9]);
                obj.GetComponent<TownCentre>().happiness = Mathf.RoundToInt(villages[i, 10]);

                for (int j = 0; j < obj.GetComponent<TownCentre>().redM; j++)
                {
                    Instantiate(mRedVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
                for (int j = 0; j < obj.GetComponent<TownCentre>().redF; j++)
                {
                    Instantiate(fRedVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
                for (int j = 0; j < obj.GetComponent<TownCentre>().blueM; j++)
                {
                    Instantiate(mBlueVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
                for (int j = 0; j < obj.GetComponent<TownCentre>().blueF; j++)
                {
                    Instantiate(fBlueVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
                for (int j = 0; j < obj.GetComponent<TownCentre>().greenM; j++)
                {
                    Instantiate(mGreenVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
                for (int j = 0; j < obj.GetComponent<TownCentre>().greenF; j++)
                {
                    Instantiate(fGreenVillager[obj.GetComponent<TownCentre>().colour], new Vector3(Random.Range(obj.transform.position.x - 20.0f, obj.transform.position.x + 20.0f), 5, Random.Range(obj.transform.position.z - 20.0f, obj.transform.position.z + 20.0f)), Quaternion.identity).GetComponent<Villager>().townCenter = obj.transform;
                }
            }
        }

        GameObject livestockSpawn = GameObject.Find("LiveStockTargets");
        int livestockSpawnNo = 0;
        for (int i = 0; i < livestock; i++)
        {
            Instantiate(livestockObj, new Vector3(Random.Range(livestockSpawn.transform.GetChild(livestockSpawnNo).position.x - 5.0f, livestockSpawn.transform.GetChild(livestockSpawnNo).position.x + 5.0f), 5, Random.Range(livestockSpawn.transform.GetChild(livestockSpawnNo).position.z - 5.0f, livestockSpawn.transform.GetChild(livestockSpawnNo).position.z + 5.0f)), Quaternion.identity);
            livestockSpawnNo++;
            if (livestockSpawnNo >= livestockSpawn.transform.childCount)
            {
                livestockSpawnNo = 0;
            }
        }

        GameObject birdSpawn = GameObject.Find("BirdTargets");
        int birdSpawnNo = 0;
        for (int i = 0; i < birds; i++)
        {
            Instantiate(bird, new Vector3(Random.Range(birdSpawn.transform.GetChild(birdSpawnNo).position.x - 5.0f, birdSpawn.transform.GetChild(birdSpawnNo).position.x + 5.0f), 5, Random.Range(birdSpawn.transform.GetChild(birdSpawnNo).position.z - 5.0f, birdSpawn.transform.GetChild(birdSpawnNo).position.z + 5.0f)), Quaternion.identity);
            birdSpawnNo++;
            if (birdSpawnNo >= birdSpawn.transform.childCount)
            {
                birdSpawnNo = 0;
            }
        }

        GameObject penguinSpawn = GameObject.Find("PenguinTargets");
        int penguinSpawnNo = 0;
        for (int i = 0; i < penguins; i++)
        {
            Instantiate(penguin, new Vector3(Random.Range(penguinSpawn.transform.GetChild(penguinSpawnNo).position.x - 5.0f, penguinSpawn.transform.GetChild(penguinSpawnNo).position.x + 5.0f), 5, Random.Range(penguinSpawn.transform.GetChild(penguinSpawnNo).position.z - 5.0f, penguinSpawn.transform.GetChild(penguinSpawnNo).position.z + 5.0f)), Quaternion.identity);
            penguinSpawnNo++;
            if (penguinSpawnNo >= penguinSpawn.transform.childCount)
            {
                penguinSpawnNo = 0;
            }
        }

    }

}
