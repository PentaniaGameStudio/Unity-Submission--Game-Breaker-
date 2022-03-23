using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string playerName = "-Empty-";
    [SerializeField] private string playerLastName = "-Empty-";

    private void Awake()
    {

    }

    private void Start()
    {
        playerName = DataSave.instance.Load("", "playerName");
        playerLastName = DataSave.instance.Load("", "playerScore");
    }

    public void StartGame()
    {
        DataSave.instance.Save(playerName, "playerName");
        DataSave.instance.Save(5, "playerScore");
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
