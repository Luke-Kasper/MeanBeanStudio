using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        LoadFile();
    }

    public float currentTotalBeanCount=0;

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(currentTotalBeanCount);

        print("saving data" + data.totalBeanCount);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);

        GameData data2 = new GameData(7);
        bf.Serialize(file, data2);

        file.Close();

    }


    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        GameData data2 = (GameData)bf.Deserialize(file);
        file.Close();



        currentTotalBeanCount = data.totalBeanCount;
        //print("test Beans" + currentTotalBeanCount);
        //print("test data2 =" + data2.totalBeanCount);


    }

}
