using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class ControlsSave
{
    public string[] bindings;
    public bool invertY;

    public ControlsSave()
    {

    }

    public ControlsSave(string[] keys, bool invert)
    {
        bindings = keys;
        invertY = invert;
        Store();
    }

    public void Store()
    {
        string path = Application.persistentDataPath + "/keyboard.pref";

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

    public ControlsSave Load()
    {
        string path = Application.persistentDataPath + "/keyboard.pref";
        FileStream file;

        if (File.Exists(path))
        {
            file = File.OpenRead(path);
        }
        else
        {
            //Here should be return what ever the default is
            string[] bindings = new string[2];
            bindings[0] = "space";
            bindings[1] = "x";
            ControlsSave standard = new ControlsSave(bindings, false);

            return standard;
        }

        BinaryFormatter bf = new BinaryFormatter();
        ControlsSave data = (ControlsSave)bf.Deserialize(file);
        file.Close();
        bindings = data.bindings;
        invertY = data.invertY;
        return this;
    }
}
