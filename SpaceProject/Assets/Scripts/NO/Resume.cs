using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject restartButton;
    public GameObject menuButton;

    public void ResumeGame()
    {
        pauseButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
