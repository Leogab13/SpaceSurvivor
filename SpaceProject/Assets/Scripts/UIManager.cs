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
    public GameObject UserDataUI;


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
    public void ClearScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        MainMenuUI.SetActive(false);
        LeaderboardUI.SetActive(false);
        UserDataUI.SetActive(false);
    }
    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);        
    }
    public void RegisterScreen() // Register button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }
    public void MainMenuScreen()
    {
        ClearScreen();
        MainMenuUI.SetActive(true);        
    }
    public void LeaderboardScreen()
    {
        ClearScreen();
        LeaderboardUI.SetActive(true);
    }
    public void UserDataScreen()
    {
        ClearScreen();
        UserDataUI.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gioco");
    }
}
