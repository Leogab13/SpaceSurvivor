using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject MainMenuUI;
    public GameObject LeaderboardUI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LeaderboardUI.SetActive(false);
    }
    public void RegisterScreen() // Register button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        MainMenuUI.SetActive(false);
        LeaderboardUI.SetActive(false);
    }
    public void MainMenuScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        MainMenuUI.SetActive(true);
        LeaderboardUI.SetActive(false);
    }
    public void LeaderboardScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LeaderboardUI.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gioco");
    }

}
