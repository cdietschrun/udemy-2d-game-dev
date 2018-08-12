using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager dataStore;

    //instance var
    public int currentLevelID;

    // persisted vars
    public int gemsCollected;
    public int highestLevelCompleted;

    void Awake()
    {
        if (dataStore == null)
        {
            DontDestroyOnLoad(gameObject);
            dataStore = this;
            Load();
        }
        else if (dataStore != this)
        {
            Destroy(gameObject);
        }
    }

    public void endGameWithWin()
    {
        if (currentLevelID > highestLevelCompleted)
        {
            highestLevelCompleted = currentLevelID;
        }
        Save();

        Debug.Log("Game over - win");
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }

    public void endGameWithLoss()
    {
        Debug.Log("Game over - loss");
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }

    // save/load
    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.dat");

        GameData data = new GameData();
        data.gemsCollectedTotal = gemsCollected;
        data.highestLevel = highestLevelCompleted;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            GameData data = (GameData)binaryFormatter.Deserialize(file);

            file.Close();

            gemsCollected = data.gemsCollectedTotal;
            highestLevelCompleted = data.highestLevel;
        }
    }
}

[Serializable]
class GameData
{
    public int gemsCollectedTotal;
    public int highestLevel;
}
