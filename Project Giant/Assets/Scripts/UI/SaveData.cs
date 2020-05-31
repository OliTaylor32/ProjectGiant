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
    public float[,] igloos;
    public float[,] bWoodWorkshops;
    public float[,] snowMen;
    public float[,] torches;
    public float[,] totems;

    public int blueVillagers;
    public int blackVillagers;


    public SaveData()
    {
        
    }

    public SaveData(float[,] treeData, float[,] iglooData, float[,] bWWData, float[,] snowMenData, float[,] torchData, float[,] totemData, int blue, int black)
    {
        trees = treeData;
        igloos = iglooData;
        bWoodWorkshops = bWWData;
        snowMen = snowMenData;
        torches = torchData;
        totems = totemData;
        blueVillagers = blue;
        blackVillagers = black;
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
