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


    public SaveData()
    {
        
    }

    public SaveData(float[,] treeData, float[,] treeWiltData, float[,] stoneData, float[,] redSmallHouseData, float[,] redFarmData, float[,] torchData, float[,] totemData, int mRed, int fRed, int livestockNo, int birdNo)
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
        Store();
    }

    public void Store()
    {

        string path = Application.persistentDataPath + "/savedata.map";
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

    public SaveData Load()
    {
        string path = Application.persistentDataPath + "/savedata.map";
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
