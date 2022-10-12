using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public float[,] trees;
    public float[,] treeWilts;
    public float[,] snowTrees;
    public float[,] snowTreeWilts;
    public float[,] stones;
    public float[,] redSmallHouses;
    public float[,] blueSmallHouses;
    public float[,] redFarms;
    public float[,] torches;
    public float[,] totems;

    public float[,] villages;

    public int livestock;
    public int bird;
    public int penguins;
    public int nature;

    public int saveSlot;


    public SaveData()
    {
        
    }

    public SaveData(float[,] treeData, float[,] treeWiltData, float[,] snowTreeData, float[,] snowTreeWiltData, float[,] stoneData, float[,] redSmallHouseData, float[,] blueSmallHouseData, float[,] redFarmData, float[,] torchData, float[,] totemData, float[,] villageData, int livestockNo, int birdNo, int penguinNo, int natureScore, int slot)
    {
        trees = treeData;
        treeWilts = treeWiltData;
        snowTrees = snowTreeData;
        snowTreeWilts = snowTreeWiltData;
        stones = stoneData;
        redSmallHouses = redSmallHouseData;
        blueSmallHouses = blueSmallHouseData;
        redFarms = redFarmData;
        torches = torchData;
        totems = totemData;
        villages = villageData;
        livestock = livestockNo;
        bird = birdNo;
        penguins = penguinNo;
        nature = natureScore;
        saveSlot = slot;
        Store();
    }

    public void Store()
    {
        string path;
        if (saveSlot == 0)
        {
             path = Application.persistentDataPath + "/tempdata.map2";

        }
        if (saveSlot == 1)
        {
            path = Application.persistentDataPath + "/savedata.map2";
        }
        if (saveSlot == 2)
        {
            path = Application.persistentDataPath + "/savedatashebbear.map2";
        }
        if (saveSlot == 3)
        {
            path = Application.persistentDataPath + "/savedatabideford.map2";
        }
        else
        {
            path = Application.persistentDataPath + "/errordata.map2";
        }
        FileStream file;

        if (File.Exists(path))
        {
            file = File.OpenWrite(path);
        }
        else
        {
            file = File.Create(path);
        }



        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, this);
        file.Close();
    }

    public SaveData Load(string map)
    {
        string path;
        if (map == "Taddiport")
        {
            path = Application.persistentDataPath + "/savedata.map2";
        }
        else if (map == "Shebbear")
        {
            path = Application.persistentDataPath + "/savedatashebbear.map2";
        }
        else
        {
            path = Application.persistentDataPath + "/savedatabideford.map2";
        }
        FileStream file;

        if (File.Exists(path))
        {
            file = File.OpenRead(path);
        }
        else
        {
            if (map == "Taddiport")
            {
                path = Application.persistentDataPath + "/savedata.map2";
            }
            else if (map == "Shebbear")
            {
                path = Application.persistentDataPath + "/savedatashebbear.map";
            }
            else
            {
                path = Application.persistentDataPath + "/savedatabideford.map";
            }

            if (File.Exists(path))
            {
                file = File.OpenRead(path);
            }
            else
            {
                //Here should be return what ever the default is
                return null;
            }
        }
    

        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();
        return data;
    }


}
