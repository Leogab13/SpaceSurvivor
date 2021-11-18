using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prendiMonete : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.Contains("Coin"))
        {
            collision.gameObject.SetActive(false);
        }

    }



}
