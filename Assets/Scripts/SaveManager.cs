using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [SerializeField] public FieldInfo dataToCheck;
    [System.Serializable]
    public class SaveData
    { 
    [SerializeField] public string playerName = "";
    [SerializeField] public int playerScore = 0;
    }
    public SaveData saveData = new SaveData();


    private void Awake()
    {
        InstanceInitialize();
    }

    private void InstanceInitialize()
    {
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
            LoadPersistantData();
        }
        else Destroy(gameObject);
    }

    private void LoadPersistantData()
    {
        string path = Application.dataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }

    private void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/savefile.json", json);
    }

    public void Save(string saving, string stringName)
    {
        dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
        if (dataToCheck.FieldType == typeof(string))
        {
            dataToCheck.SetValue(saveData, saving);
        }
        else Debug.Log("Type Error, string wanted");
    }

    public void Save(int saving, string stringName)
    {
        dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
        if (dataToCheck.FieldType == typeof(int))
        {
            dataToCheck.SetValue(saveData, saving);
        }
        else Debug.Log("Type Error, int wanted");
    }

    public string Load(string baseValue, string stringName)
    {
        dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
        if (dataToCheck.FieldType == typeof(string))
        {
            if (dataToCheck != null)
            {
                baseValue = (string)dataToCheck.GetValue(saveData);
                return baseValue;
            }
        }
        return baseValue;
    }

    public int Load(int baseValue, string stringName)
    {
        dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
        if (dataToCheck.FieldType == typeof(int))
        {
            if (dataToCheck != null)
            {
                baseValue = (int)dataToCheck.GetValue(saveData);
                return baseValue;
            }
        }
        return baseValue;
    }
}
