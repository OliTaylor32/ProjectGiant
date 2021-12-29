using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public float[,] treeData;
    public GameObject[] trees;
    public int treeNo = 0;

    public float[,] iglooData;
    public int iglooNo = 0;
    public GameObject[] igloos;

    public float[,] bWoodWorkshopData;
    public int bWWNo = 0;
    public GameObject[] bWoodWorkshops;

    public float[,] snowMenData;
    public int snowMenNo = 0;
    public GameObject[] snowMen;

    public float[,] torchData;
    public int torchNo = 0;
    public GameObject[] torches;

    public float[,] totemData;
    public int totemNo = 0;
    public GameObject[] totems;

    public int blueVillagers = 0;
    public int blackVillagers = 0;

    // Start is called before the first frame update
    void Start()
    {
        save();
        treeNo = 0;
        iglooNo = 0;
        bWWNo = 0;
        snowMenNo = 0;
        blueVillagers = 0;
        torchNo = 0;
        totemNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void save()
    {
        foreach (var gameObject in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObject.GetComponent<Object>() != null)
            {
                //print("Object discovered");
                switch (gameObject.GetComponent<Object>().item)
                {
                    case "tree":
                        AddTree(gameObject);
                        break;
                    case "sapling":
                        AddTree(gameObject);
                        break;
                    case "igloo":
                        AddIgloo(gameObject);
                        break;
                    case "bWoodWorkshop":
                        AddBWW(gameObject);
                        break;
                    case "snowMan":
                        AddSnowMan(gameObject);
                        break;
                    case "torch":
                        AddTorch(gameObject);
                        break;
                    case "totem":
                        AddTotem(gameObject);
                        break;
                    default:
                        break;
                }
            }

            if (gameObject.GetComponent<Villager>() != null)
            {
                switch (gameObject.GetComponent<Villager>().colour)
                {
                    case "Blue":
                        blueVillagers++;
                        break;

                    case "Black":
                        blackVillagers++;
                        break;
                    default:
                        break;
                }
            }
        }

        if (trees != null)
        {
            treeData = new float[trees.Length, 3];
            for (int i = 0; i < trees.Length; i++)
            {
                treeData[i, 0] = trees[i].transform.position.x;
                treeData[i, 1] = trees[i].transform.position.z;
                treeData[i, 2] = trees[i].transform.rotation.y;
            }
        }

        if (igloos != null)
        {
            iglooData = new float[igloos.Length, 3];
            for (int i = 0; i < igloos.Length; i++)
            {
                iglooData[i, 0] = igloos[i].transform.position.x;
                iglooData[i, 1] = igloos[i].transform.position.z;
                iglooData[i, 2] = igloos[i].transform.rotation.y;
            }
        }

        if (bWoodWorkshops != null)
        {
            bWoodWorkshopData = new float[bWoodWorkshops.Length, 3];
            for (int i = 0; i < bWoodWorkshops.Length; i++)
            {
                bWoodWorkshopData[i, 0] = bWoodWorkshops[i].transform.position.x;
                bWoodWorkshopData[i, 0] = bWoodWorkshops[i].transform.position.z;
                bWoodWorkshopData[i, 0] = bWoodWorkshops[i].transform.rotation.y;
            }
        }

        if (snowMen != null)
        {
            snowMenData = new float[snowMen.Length, 3];
            for (int i = 0; i < snowMen.Length; i++)
            {
                snowMenData[i, 0] = snowMen[i].transform.position.x;
                snowMenData[i, 0] = snowMen[i].transform.position.z;
                snowMenData[i, 0] = snowMen[i].transform.rotation.y;
            }
        }

        if (torches != null)
        {
            torchData = new float[torches.Length, 3];
            for (int i = 0; i < torches.Length; i++)
            {
                torchData[i, 0] = torches[i].transform.position.x;
                torchData[i, 0] = torches[i].transform.position.z;
                torchData[i, 0] = torches[i].transform.rotation.y;
            }
        }

        if (totems != null)
        {
            totemData = new float[totems.Length, 3];
            for (int i = 0; i < totems.Length; i++)
            {
                totemData[i, 0] = totems[i].transform.position.x;
                totemData[i, 0] = totems[i].transform.position.z;
                totemData[i, 0] = totems[i].transform.rotation.y;
            }
        }

        new SaveData(treeData, iglooData, bWoodWorkshopData, snowMenData, torchData, totemData, blueVillagers, blackVillagers);
        print("Created saveData");
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

    private void AddIgloo(GameObject newIgloo)
    {
        if (iglooNo == 0)
        {
            igloos = new GameObject[1];
            igloos[0] = newIgloo;
            iglooNo++;
        }
        else
        {
            iglooNo++;
            GameObject[] temp = igloos;
            igloos = new GameObject[iglooNo];
            igloos[0] = newIgloo;
            for (int i = 1; i < igloos.Length; i++)
            {
                igloos[i] = temp[i - 1];
            }
        }

    }

    private void AddBWW(GameObject newBWW)
    {
        if (treeNo == 0)
        {
            bWoodWorkshops = new GameObject[1];
            bWoodWorkshops[0] = newBWW;
            bWWNo++;
        }
        else
        {
            bWWNo++;
            GameObject[] temp = bWoodWorkshops;
            bWoodWorkshops = new GameObject[bWWNo];
            bWoodWorkshops[0] = newBWW;
            for (int i = 1; i < bWoodWorkshops.Length; i++)
            {
                bWoodWorkshops[i] = temp[i - 1];
            }
        }

    }

    private void AddSnowMan(GameObject newSnowman)
    {
        if (snowMenNo == 0)
        {
            snowMen = new GameObject[1];
            snowMen[0] = newSnowman;
            snowMenNo++;
        }
        else
        {
            snowMenNo++;
            GameObject[] temp = snowMen;
            snowMen = new GameObject[snowMenNo];
            snowMen[0] = newSnowman;
            for (int i = 1; i < snowMen.Length; i++)
            {
                snowMen[i] = temp[i - 1];
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
}
