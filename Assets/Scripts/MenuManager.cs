using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string playerName = "-Empty-";

    private void Awake()
    {

    }

    private void Start()
    {
        playerName = SaveManager.instance.Load("", "playerName");
    }

    public void StartGame()
    {
        SaveManager.instance.Save(playerName, "playerName");
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
