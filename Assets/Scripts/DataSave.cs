using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public static DataSave instance;
    [SerializeField] public string playerName = "";
    [SerializeField] public bool playerLastName = true;
    [SerializeField] public int playerScore;
    [SerializeField] public FieldInfo dataToCheck;


    private void Awake()
    {
        InstanceInitialize();
    }

    private void InstanceInitialize()
    {
        if (instance == null) {instance = this; DontDestroyOnLoad(gameObject);}
        else Destroy(gameObject);
    }

    public void Save(string data, string stringName)
    {
        dataToCheck = instance.GetType().GetField(stringName);
        if (dataToCheck.FieldType == typeof(string))
        {
            dataToCheck.SetValue(this, data);
        }
        else Debug.Log("Type Error, string wanted");
    }

    public string Load(string baseValue, string stringName)
    {
        dataToCheck = instance.GetType().GetField(stringName);
        if (dataToCheck.FieldType == typeof(string))
        {
            if (dataToCheck != null)
            {
                baseValue = (string)dataToCheck.GetValue(this);
                return baseValue;
            }
        }
        return baseValue;
    }

    public void Save(int data, string stringName)
    {
        dataToCheck = instance.GetType().GetField(stringName);
        if (dataToCheck.FieldType == typeof(int))
        {
            dataToCheck.SetValue(this, data);
        }
        else Debug.Log("Type Error, string wanted");
    }

    public int Load(int baseValue, string stringName)
    {
        dataToCheck = instance.GetType().GetField(stringName);
        if (dataToCheck.FieldType == typeof(int))
        {
            if (dataToCheck != null)
            {
                baseValue = (int)dataToCheck.GetValue(this);
                return baseValue;
            }
        }
        return baseValue;
    }

}
