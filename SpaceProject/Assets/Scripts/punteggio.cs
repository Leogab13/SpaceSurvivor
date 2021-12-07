using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class punteggio : MonoBehaviour
{
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameController.partita == true)  //gameover
        {
            GetComponent<Text>().text = score.ToString();

            score = score + 10;
        }
        
    }
}
