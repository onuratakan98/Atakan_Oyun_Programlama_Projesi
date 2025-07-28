using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{
   
    public static void SaveData(ScPlayerData PlayerSaveData)
    {
        Debug.Log("Save Location:  "+Application.persistentDataPath);
        string saveFilename = "GameSaveData.json";
        //string fullPath = Application.persistentDataPath + "/" + saveFilename; //
        string fullPath = Path.Combine(Application.persistentDataPath, saveFilename);


        //Make a Copy instance playerdata object
        ScPlayerData clonePxData = new ScPlayerData(PlayerSaveData); //

         
        // BinaryFormatter formatter = new BinaryFormatter();  //
        //FileStream fstream = new FileStream(fullPath,FileMode.Create); //
        //formatter.Serialize(fstream, clonePxData); //
       // fstream.Close(); //

        string jsonPlayerData = JsonUtility.ToJson(clonePxData); //
        File.WriteAllText(fullPath, jsonPlayerData); //
        
        Debug.Log("Player data is saved succesfully.");
    }

    
    
    public static ScPlayerData LoadData()
    {
        string saveFilename = "GameSaveData.json";
        //string fullPath = Application.persistentDataPath + "/" + saveFilename;
        string fullPath = Path.Combine(Application.persistentDataPath, saveFilename);
        
        if(File.Exists(fullPath))
        {
             //BinaryFormatter formatter = new BinaryFormatter(); //
           //FileStream fstream = new FileStream(fullPath, FileMode.Open); //

            // ScPlayerData clonePxData = formatter.Deserialize(fstream) as ScPlayerData; //
             //fstream.Close(); //

            string jsonLoadFile = File.ReadAllText(fullPath); //
            ScPlayerData clonePxdata = new ScPlayerData(); //
            JsonUtility.FromJsonOverwrite(jsonLoadFile, clonePxdata); //

            Debug.Log("Player data is loaded succesfully.");
             return clonePxdata; 
        }
        else
        {
            Debug.LogError("Error: Save file not found in" + fullPath);
        }
        
        
        return null;
    }

} 
