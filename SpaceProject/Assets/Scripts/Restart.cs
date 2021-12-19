using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Riavvia()
    {
        SceneManager.LoadScene(0);
    }
}
