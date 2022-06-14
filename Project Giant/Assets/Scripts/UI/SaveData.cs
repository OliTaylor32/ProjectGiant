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
    public float[,] stones;
    public float[,] redSmallHouses;
    public float[,] redFarms;
    public float[,] torches;
    public float[,] totems;

    public int mRedVillagers;
    public int fRedVillagers;

    public int livestock;
    public int bird;

    public int saveSlot;


    public SaveData()
    {
        
    }

    public SaveData(float[,] treeData, float[,] treeWiltData, float[,] stoneData, float[,] redSmallHouseData, float[,] redFarmData, float[,] torchData, float[,] totemData, int mRed, int fRed, int livestockNo, int birdNo, int slot)
    {
        trees = treeData;
        treeWilts = treeWiltData;
        stones = stoneData;
        redSmallHouses = redSmallHouseData;
        redFarms = redFarmData;
        torches = torchData;
        totems = totemData;
        mRedVillagers = mRed;
        fRedVillagers = fRed;
        livestock = livestockNo;
        bird = birdNo;
        saveSlot = slot;
        Store();
    }

    public void Store()
    {
        string path;
        if (saveSlot == 0)
        {
             path = Application.persistentDataPath + "/tempdata.map";

        }
        if (saveSlot == 1)
        {
            path = Application.persistentDataPath + "/savedata.map";
        }
        if (saveSlot == 2)
        {
            path = Application.persistentDataPath + "/savedatashebbear.map";
        }
        else
        {
            path = Application.persistentDataPath + "/errordata.map";
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
            path = Application.persistentDataPath + "/savedata.map";
        }
        else
        {
            path = Application.persistentDataPath + "/savedatashebbear.map";
        }
        FileStream file;

        if (File.Exists(path)) 
        {
            file = File.OpenRead(path);
        }
        else
        {
            //Here should be return what ever the default is
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();
        return data;
    }


}
