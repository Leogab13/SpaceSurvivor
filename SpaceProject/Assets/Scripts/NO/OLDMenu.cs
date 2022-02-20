using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OLDMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gioco");
    }

    public void LoadLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
