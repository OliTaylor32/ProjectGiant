using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public float[,] treeData;
    public GameObject[] trees;
    public int treeNo = 0;

    public float[,] treeWiltData;
    public GameObject[] treeWilts;
    public int treeWiltNo = 0;

    public float[,] snowTreeData;
    public GameObject[] snowTrees;
    public int snowTreeNo = 0;

    public float[,] snowTreeWiltData;
    public GameObject[] snowTreeWilts;
    public int snowTreeWiltNo = 0;

    private bool wilting;

    public float[,] stoneData;
    public int stoneNo = 0;
    public GameObject[] stones;

    public float[,] redSmallHouseData;
    public int redSmallHouseNo = 0;
    public GameObject[] redSmallHouses;

    public float[,] blueSmallHouseData;
    public int blueSmallHouseNo = 0;
    public GameObject[] blueSmallHouses;

    public float[,] redFarmData;
    public int redFarmNo = 0;
    public GameObject[] redFarms;

    public float[,] torchData;
    public int torchNo = 0;
    public GameObject[] torches;

    public float[,] totemData;
    public int totemNo = 0;
    public GameObject[] totems;

    public float[,] villageData;
    public int villageNo;
    public GameObject[] villages;

    public int livestock = 0;
    public int birds = 0;
    public int penguins = 0;

    // Start is called before the first frame update
    void Start()
    {
        wilting = true;
        treeNo = 0;
        treeWiltNo = 0;
        snowTreeNo = 0;
        snowTreeWiltNo = 0;
        stoneNo = 0;
        redSmallHouseNo = 0;
        blueSmallHouseNo = 0;
        redFarmNo = 0;
        villageNo = 0;
        torchNo = 0;
        totemNo = 0;
        livestock = 0;
        birds = 0;
        StartCoroutine(DelayedSave());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void save()
    {
        wilting = true;
        treeNo = 0;
        treeWiltNo = 0;
        stoneNo = 0;
        redSmallHouseNo = 0;
        redFarmNo = 0;
        torchNo = 0;
        totemNo = 0;
        livestock = 0;
        birds = 0;

        foreach (var gameObject in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObject.GetComponent<Object>() != null)
            {
                //print("Object discovered");
                switch (gameObject.GetComponent<Object>().item)
                {
                    case "tree":
                        if (wilting == false)
                        {
                            switch (gameObject.GetComponent<Object>().treeType)
                            {
                                case 0:
                                    AddTree(gameObject);
                                    break;
                                case 1:
                                    AddSnowTree(gameObject);
                                    break;
                                default:
                                    break;
                            }
                            wilting = true;
                        }
                        else
                        {
                            switch (gameObject.GetComponent<Object>().treeType)
                            {
                                case 0:
                                    AddTreeWilt(gameObject);
                                    break;
                                case 1:
                                    AddSnowTreeWilt(gameObject);
                                    break;
                                default:
                                    break;
                            }
                            wilting = false;
                        }
                        break;
                    case "sapling":
                        switch (gameObject.GetComponent<Object>().treeType)
                        {
                            case 0:
                                AddTree(gameObject);
                                break;
                            case 1:
                                AddSnowTree(gameObject);
                                break;
                            default:
                                break;
                        }

                        break;
                    case "stone":
                        AddStone(gameObject);
                        break;
                    case "sHouse":
                        AddRedSmallHouse(gameObject);
                        break;
                    case "igloo":
                        AddBlueSmallHouse(gameObject);
                        break;
                    case "farm":
                        AddRedFarm(gameObject);
                        break;
                    case "torch":
                        AddTorch(gameObject);
                        break;
                    case "totem":
                        AddTotem(gameObject);
                        break;
                    case "sheep":
                        if (gameObject.GetComponent<LiveStockAI>() != null)
                        {
                            livestock++;
                        }
                        break;
                    case "penguin":
                        penguins++;
                        break;
                    case "bird":
                        birds++;
                        break;
                    case "scaffolding":
                        //Not sure what to do about this yet.
                        break;
                    default:
                        break;
                }
            }

            if (gameObject.GetComponent<TownCentre>() != null)
            {
                AddVillage(gameObject);
            }
        }
        //Left Off work HERE, need to make new ifs for new items. ALL ELSE IS DONE.
        if (trees != null)
        {
            treeData = new float[trees.Length, 3];
            for (int i = 0; i < trees.Length; i++)
            {
                treeData[i, 0] = trees[i].transform.position.x;
                treeData[i, 1] = trees[i].transform.position.z;
                treeData[i, 2] = trees[i].transform.eulerAngles.y;
            }
        }

        if (treeWilts != null)
        {
            treeWiltData = new float[treeWilts.Length, 3];
            for (int i = 0; i < treeWilts.Length; i++)
            {
                treeWiltData[i, 0] = treeWilts[i].transform.position.x;
                treeWiltData[i, 1] = treeWilts[i].transform.position.z;
                treeWiltData[i, 2] = treeWilts[i].transform.eulerAngles.y;
            }
        }

        if (stones != null)
        {
            stoneData = new float[stones.Length, 3];
            for (int i = 0; i < stones.Length; i++)
            {
                stoneData[i, 0] = stones[i].transform.position.x;
                stoneData[i, 1] = stones[i].transform.position.z;
                stoneData[i, 2] = stones[i].transform.eulerAngles.y;
            }
        }

        if (redSmallHouses != null)
        {
            redSmallHouseData = new float[redSmallHouses.Length, 3];
            for (int i = 0; i < redSmallHouses.Length; i++)
            {
                redSmallHouseData[i, 0] = redSmallHouses[i].transform.position.x;
                redSmallHouseData[i, 1] = redSmallHouses[i].transform.position.z;
                redSmallHouseData[i, 2] = redSmallHouses[i].transform.eulerAngles.y;
            }
        }

        if (redFarms != null)
        {
            redFarmData = new float[redFarms.Length, 3];
            for (int i = 0; i < redFarms.Length; i++)
            {
                redFarmData[i, 0] = redFarms[i].transform.position.x;
                redFarmData[i, 1] = redFarms[i].transform.position.z;
                redFarmData[i, 2] = redFarms[i].transform.eulerAngles.y;
            }
        }

        if (torches != null)
        {
            torchData = new float[torches.Length, 3];
            for (int i = 0; i < torches.Length; i++)
            {
                torchData[i, 0] = torches[i].transform.position.x;
                torchData[i, 1] = torches[i].transform.position.z;
                torchData[i, 2] = torches[i].transform.eulerAngles.y;
            }
        }

        if (totems != null)
        {
            totemData = new float[totems.Length, 3];
            for (int i = 0; i < totems.Length; i++)
            {
                totemData[i, 0] = totems[i].transform.position.x;
                totemData[i, 1] = totems[i].transform.position.z;
                totemData[i, 2] = totems[i].transform.eulerAngles.y;
            }
        }

        if (birds > 10)
        {
            birds = birds - 3;
        }
        else if (birds > 5)
        {
            birds = birds - 2;
        }
        else if (birds > 0)
        {
            birds = birds - 1;
        }

        int slot = 0;
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Taddiport" || SceneManager.GetActiveScene().name == "TaddiportLoad")
        {
            print("Save As Taddiport Data");
            slot = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Shebbear" || SceneManager.GetActiveScene().name == "ShebbearLoad")
        {
            print("Save As Shebbear Data");
            slot = 2;
        }
        print(slot);
        new SaveData(treeData, treeWiltData, stoneData, redSmallHouseData, redFarmData, torchData, totemData, mRedVillagers, fRedVillagers, livestock, birds, slot);
    }

    private void AddTree(GameObject newTree)
    {
        if (treeNo == 0)
        {
            trees = new GameObject[1];
            trees[0] = newTree;
            treeNo++;
        }
        else
        {
            treeNo++;
            GameObject[] temp = trees;
            trees = new GameObject[treeNo];
            trees[0] = newTree;
            for (int i = 1; i < trees.Length; i++)
            {
                trees[i] = temp[i - 1];
            }
        }

    }

    private void AddTreeWilt(GameObject newTree)
    {
        if (treeWiltNo == 0)
        {
            treeWilts = new GameObject[1];
            treeWilts[0] = newTree;
            treeWiltNo++;
        }
        else
        {
            treeWiltNo++;
            GameObject[] temp = treeWilts;
            treeWilts = new GameObject[treeWiltNo];
            treeWilts[0] = newTree;
            for (int i = 1; i < treeWilts.Length; i++)
            {
                treeWilts[i] = temp[i - 1];
            }
        }

    }

    private void AddSnowTree(GameObject newTree)
    {
        if (snowTreeNo == 0)
        {
            snowTrees = new GameObject[1];
            snowTrees[0] = newTree;
            snowTreeNo++;
        }
        else
        {
            snowTreeNo++;
            GameObject[] temp = snowTrees;
            snowTrees = new GameObject[snowTreeNo];
            snowTrees[0] = newTree;
            for (int i = 1; i < snowTrees.Length; i++)
            {
                snowTrees[i] = temp[i - 1];
            }
        }

    }

    private void AddSnowTreeWilt(GameObject newTree)
    {
        if (snowTreeWiltNo == 0)
        {
            snowTreeWilts = new GameObject[1];
            snowTreeWilts[0] = newTree;
            snowTreeWiltNo++;
        }
        else
        {
            snowTreeWiltNo++;
            GameObject[] temp = snowTreeWilts;
            snowTreeWilts = new GameObject[snowTreeWiltNo];
            snowTreeWilts[0] = newTree;
            for (int i = 1; i < snowTreeWilts.Length; i++)
            {
                snowTreeWilts[i] = temp[i - 1];
            }
        }

    }

    private void AddStone(GameObject newIgloo)
    {
        if (stoneNo == 0)
        {
            stones = new GameObject[1];
            stones[0] = newIgloo;
            stoneNo++;
        }
        else
        {
            stoneNo++;
            GameObject[] temp = stones;
            stones = new GameObject[stoneNo];
            stones[0] = newIgloo;
            for (int i = 1; i < stones.Length; i++)
            {
                stones[i] = temp[i - 1];
            }
        }

    }

    private void AddRedSmallHouse(GameObject newRedSmallHouse)
    {
        if (redSmallHouseNo == 0)
        {
            redSmallHouses = new GameObject[1];
            redSmallHouses[0] = newRedSmallHouse;
            redSmallHouseNo++;
        }
        else
        {
            redSmallHouseNo++;
            GameObject[] temp = redSmallHouses;
            redSmallHouses = new GameObject[redSmallHouseNo];
            redSmallHouses[0] = newRedSmallHouse;
            for (int i = 1; i < redSmallHouses.Length; i++)
            {
                redSmallHouses[i] = temp[i - 1];
            }
        }

    }

    private void AddBlueSmallHouse(GameObject newBlueSmallHouse)
    {
        if (blueSmallHouseNo == 0)
        {
            blueSmallHouses = new GameObject[1];
            blueSmallHouses[0] = newBlueSmallHouse;
            blueSmallHouseNo++;
        }
        else
        {
            blueSmallHouseNo++;
            GameObject[] temp = blueSmallHouses;
            blueSmallHouses = new GameObject[blueSmallHouseNo];
            blueSmallHouses[0] = newBlueSmallHouse;
            for (int i = 1; i < blueSmallHouses.Length; i++)
            {
                blueSmallHouses[i] = temp[i - 1];
            }
        }

    }

    private void AddRedFarm(GameObject newRedFarm)
    {
        if (redFarmNo == 0)
        {
            redFarms = new GameObject[1];
            redFarms[0] = newRedFarm;
            redFarmNo++;
        }
        else
        {
            redFarmNo++;
            GameObject[] temp = redFarms;
            redFarms = new GameObject[redFarmNo];
            redFarms[0] = newRedFarm;
            for (int i = 1; i < redFarms.Length; i++)
            {
                redFarms[i] = temp[i - 1];
            }
        }

    }

    private void AddTorch(GameObject newTorch)
    {
        if (torchNo == 0)
        {
            torches = new GameObject[1];
            torches[0] = newTorch;
            torchNo++;
        }
        else
        {
            torchNo++;
            GameObject[] temp = torches;
            torches = new GameObject[torchNo];
            torches[0] = newTorch;
            for (int i = 1; i < torches.Length; i++)
            {
                torches[i] = temp[i - 1];
            }
        }

    }

    private void AddTotem(GameObject newTotem)
    {
        if (totemNo == 0)
        {
            totems = new GameObject[1];
            totems[0] = newTotem;
            totemNo++;
        }
        else
        {
            totemNo++;
            GameObject[] temp = totems;
            totems = new GameObject[totemNo];
            totems[0] = newTotem;
            for (int i = 1; i < totems.Length; i++)
            {
                totems[i] = temp[i - 1];
            }
        }

    }

    private void AddVillage(GameObject newVillage)
    {
        if (villageNo == 0)
        {
            villages = new GameObject[1];
            villages[0] = newVillage;
            villageNo++;
        }
        else
        {
            villageNo++;
            GameObject[] temp = villages;
            villages = new GameObject[villageNo];
            villages[0] = newVillage;
            for (int i = 1; i < villages.Length; i++)
            {
                villages[i] = temp[i - 1];
            }
        }
    }

    private IEnumerator DelayedSave()
    {
        yield return new WaitForSeconds(1f);
        save();
    }
}
