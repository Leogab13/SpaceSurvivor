using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip shieldSound;
    public AudioClip shieldDownSound;
 


    Rigidbody2D rb;                     //corpo nave

    private float startingSpeed = 1.5f;  // velocit� di partenza nave
    private float speed;                 //velocit� nave

    Animator animator;

    float asseX;

    private GameObject shield;
    private bool shieldActive;
    private bool tempShieldActive;

    private Vector2 shipPosition;

    public int life;   // 3 vite a partita , viene decrementato di 1 ad ogni collisione , a 0=gameover , uso life anche per gestire la healthbar

    //di seguito le 3 barre salute
    public GameObject hbarHigh;     //salute massima = 3
    public GameObject hbarMedium;   //salute media = 2
    public GameObject hbarLow;      //salute minima =1
    
    public static bool dead;          //false se la nave ha almeno una vita, true dopo l'ultima collisione

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        speed = startingSpeed;
        life = 3;
        dead = false;

        shield = this.transform.Find("shield").gameObject;
        shield.SetActive(false);
        shieldActive = false;

    }

    void FixedUpdate()      //con il fixed update controllo i movimenti della nave
    {
        Vector2 position = rb.position;                                                 //leggo la posizione del rigidbody della nave

        if ( !(position.x <= -1.56 && asseX < 0) && !(position.x >= 1.56 && asseX > 0) )  //se non sto uscendo dai margini della telecamera
        {
            position.x = position.x + asseX * Time.deltaTime * speed;                   //calcolo la nuova posizione della nave sull'asse x in funzione dell'input, della velocit� e del tempo trascorso
            rb.MovePosition(position);                                                  //sposto la nave
        }
        speed = startingSpeed * GameController.speedFactor;                             //calcolo la nuova velocit� accelerata
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (TouchInput.touched)
            {
                asseX = TouchInput.axis;
            }
            else
            {
                asseX = Input.GetAxisRaw("Horizontal"); //leggo l'input
            }
        }

        if (Mathf.Approximately(asseX, 0.0f))   //se non c'� movimento
        {
            animator.SetBool("Stopped", true);  //attivo l'animazione della nave frontale
        }
        else                                    //se c'� movimento
        {
            animator.SetBool("Stopped", false); //disattivo l'animazione della nave frontale
        }
        animator.SetFloat("MoveX", asseX);      //il parametro MoveX gestisce le animazioni della nave inclinata a destra o a sinistra

        //gestione healthbar:
        if (life == 3)
        {
            hbarHigh.SetActive(true);   
            hbarMedium.SetActive(false);
            hbarLow.SetActive(false);
        }
        if (life == 2)
        {
            hbarHigh.SetActive(false);
            hbarMedium.SetActive(true);
            hbarLow.SetActive(false);
        }
        if (life == 1)
        {
            hbarHigh.SetActive(false);
            hbarMedium.SetActive(false);
            hbarLow.SetActive(true);
        }
        /*if (life == 0)
        {
            hbarHigh.SetActive(false);
            hbarMedium.SetActive(false);
            hbarLow.SetActive(false);
        }*/


        tempShieldActive = shieldActive;
        shieldActive = shield.activeSelf;   //controllo se lo scudo � attivo
        if (tempShieldActive && !shieldActive)
        {
            audioSource.PlayOneShot(shieldDownSound);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)   //gestione collisioni
    {

        if (collision.name.Contains("Coin"))
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(coinSound);
            Punteggio.score = Punteggio.score + 2000;
        }
        if (collision.name.Contains("Redcoin"))
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(coinSound);
            Punteggio.score = Punteggio.score + 10000;
        }
        if (collision.name.Contains("Asteroid") && !shieldActive)
        {
            shipPosition = this.transform.position;
            GameController.explosions[GameController.explosionIndex].transform.position = shipPosition;
            GameController.explosions[GameController.explosionIndex].SetActive(true);
            GameController.explosionIndex++;
            if (GameController.explosionIndex >= GameController.explosions.Length)
            {
                GameController.explosionIndex = 0;
            }
            collision.gameObject.SetActive(false);
          
            life--;    //decremento di 1 la vita
          
            if (life == 0)
            {
                hbarLow.SetActive(false);
                dead = true;
        
            }

        }
        if (collision.name.Contains("HealthCollectible"))
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(healthSound);
            if (life < 3)
            {
                life++;
            }
        }
        if (collision.name.Contains("ShieldCollectible"))
        {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(shieldSound);

            if (shieldActive)
            {
                ShieldController.timer = ShieldController.totalTime;
            }
            else
            {
                shield.SetActive(true);
                shieldActive = true;
            }
        }
    }






}
