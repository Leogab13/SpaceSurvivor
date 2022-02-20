using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject menuButton;
    public void PauseGame()
    {
        Time.timeScale = 0;
        resumeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
