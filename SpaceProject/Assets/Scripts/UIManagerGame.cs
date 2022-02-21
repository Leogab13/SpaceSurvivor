using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerGame : MonoBehaviour
{
    public static UIManagerGame instance;

    public GameObject pauseButton;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject resumeButton;
    public GameObject recordTitle;
    public TMP_Text record;
    public GameObject restartButtonOver;
    public GameObject menuButtonOver;
    public GameObject gameOver;


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

    /*public void ClearScreen()
    {
        pauseButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);

    }*/

    private void Start()
    {
        record.text = GameController.highScore.ToString();
    }

    public void ResumeGame()
    {
        recordTitle.gameObject.SetActive(false);
        record.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false); pauseButton.gameObject.SetActive(true);        
        Time.timeScale = 1;       
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gioco");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        recordTitle.gameObject.SetActive(true);
        record.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void GameOverScreen()
    {
        pauseButton.gameObject.SetActive(false);
        restartButtonOver.gameObject.SetActive(true);
        menuButtonOver.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
        recordTitle.gameObject.SetActive(true);
        record.gameObject.SetActive(true);
    }


}
