using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    AudioSource audioSource;

    Rigidbody2D rb;                     //corpo nave

    public float startingSpeed = 3.0f;  // velocità di partenza nave
    public float speed;                 //velocità nave

    Animator animator;

    float asseX;

    public GameObject explosion;

    private GameObject shield;
    private bool shieldActive;

    public int life = 3;   // 3 vite a partita , viene decrementato di 1 ad ogni collisione , a 0=gameover , uso life anche per gestire la healthbar

    //di seguito le 3 barre salute
    public GameObject hbarHigh;     //salute massima = 3
    public GameObject hbarMedium;   //salute media = 2
    public GameObject hbarLow;      //salute minima =1

    public GameObject gameOver;         //la sprite della scritta GAME OVER
    private float timeOfDeath;          //il momento della morte
    private float deathDelay = 1.0f;    //il ritardo tra l'ultima collisione e la fine della partita
    private bool dead = false;          //false se la nave ha almeno una vita, true dopo l'ultima collisione
    public static bool partita = true;  //gestione della partita, true=vivo false=gameover

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = startingSpeed;
        shield = this.transform.Find("shield").gameObject;
        shield.SetActive(false);
        shieldActive = false;
    }

    void FixedUpdate()      //con il fixed update controllo i movimenti della nave
    {
        Vector2 position = rb.position;                                                 //leggo la posizione del rigidbody della nave

        if (!(position.x <= -1.56 && asseX < 0) && !(position.x >= 1.56 && asseX > 0))  //se non sto uscendo dai margini della telecamera
        {
            position.x = position.x + asseX * Time.deltaTime * speed;                   //calcolo la nuova posizione della nave sull'asse x in funzione della velocità e del tempo trascorso
            rb.MovePosition(position);                                                  //sposto la nave
        }
        speed = startingSpeed * GameController.speedFactor;                             //calcolo la nuova velocità accelerata
    }

    // Update is called once per frame
    void Update()
    {
        asseX = Input.GetAxisRaw("Horizontal"); //leggo l'input

        if (Mathf.Approximately(asseX, 0.0f))   //se non c'è movimento
        {
            animator.SetBool("Stopped", true);  //attivo l'animazione della nave frontale
        }
        else                                    //se c'è movimento
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
        if (life == 0)
        {
            hbarHigh.SetActive(false);
            hbarMedium.SetActive(false);
            hbarLow.SetActive(false);
        }

        if (dead && Time.time>=(timeOfDeath+deathDelay)) //dopo un ritardo dal momento della morte
        {
            gameObject.SetActive(false);                 //faccio scomparire la nave
            partita = false;                             //fermo tutti gli oggetti mobili
        }

        shieldActive = shield.activeSelf;   //controllo se lo scudo è attivo
    }




    private void OnTriggerEnter2D(Collider2D collision)   //gestione collisioni
    {

        if (collision.name.Contains("Coin"))
        {
            collision.gameObject.SetActive(false);
            punteggio.score = punteggio.score + 2000;
        }
        if (collision.name.Contains("Asteroid") && !shieldActive)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            audioSource.Play();
          
            life--;    //decremento di 1 la vita
          
            if (life == 0)
            {
                dead = true;
                timeOfDeath = Time.time;
                gameOver.SetActive(true);      //gameover
            }

        }
        if (collision.name.Contains("HealthCollectible"))
        {
            collision.gameObject.SetActive(false);
            if (life < 3)
            {
                life++;
            }
        }
        if (collision.name.Contains("ShieldCollectible"))
        {
            if (shieldActive)
            {
                ShieldController.timer = ShieldController.totalTime;
            }
            else
            {
                collision.gameObject.SetActive(false);
                shield.SetActive(true);
                shieldActive = true;
            }
        }
    }






}
