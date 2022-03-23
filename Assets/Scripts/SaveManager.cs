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
        [SerializeField] public string bestPlayerName = "-Empty-";
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
        string path = Application.dataPath + "/Save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }

    private void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/Save.json", json);
    }

    private bool TryVariableName(string stringName)
    {
        bool result = false;
        foreach(FieldInfo field in typeof(SaveManager.SaveData).GetFields())
        {
            if (field.Name == stringName) result = true;
        }
        return result;
        
    }

    public void Save(string saving, string stringName)
    {
        if (TryVariableName(stringName) == true)
        {
            dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
            if (dataToCheck.FieldType == typeof(string))
            {
                dataToCheck.SetValue(saveData, saving);
            }
            else Debug.Log("Type Error, string wanted");
        }
        else Debug.Log("Name Error, your string don't exist");
    }

    public void Save(int saving, string stringName)
    {
        if (TryVariableName(stringName) == true)
        {
                dataToCheck = typeof(SaveManager.SaveData).GetField(stringName);
                if (dataToCheck.FieldType == typeof(int))
                {
                    dataToCheck.SetValue(saveData, saving);
                }
                else Debug.Log("Type Error, integer wanted");
                       
        }
        else Debug.Log("Name Error, your integer don't exist");
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
