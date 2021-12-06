using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    AudioSource audioSource;

    Rigidbody2D rb;              //corpo nave
    public float startingSpeed = 3.0f;// velocit� di partenza nave
    public float speed;    //velocit� nave
    Animator animator;
    float asseX;
    public GameObject explosion;
    private GameObject shield;
    private bool shieldActive;
    public GameObject gameOver;
    public GameObject laMiaNave;

    //di seguito le 3 barre salute
    public GameObject hbarHigh;
    public GameObject hbarMedium;
    public GameObject hbarLow;


    public int life = 3;   // 3 vite a partita , viene decrementato di 1 ad ogni collisione , a 0=gameover , uso life anche per gestire la healthbar
    public static bool partita = true;      //gestione della partita, true=vivo false=gameover




    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = startingSpeed;
        shield = laMiaNave.transform.Find("shield").gameObject;
        shield.SetActive(false);
        shieldActive = false;
    }

    void FixedUpdate()      //con il fixed update controllo i movimenti della nave
    {
        Vector2 position = rb.position;

        if (!(position.x <= -1.56 && asseX < 0) && !(position.x >= 1.56 && asseX > 0))//se non sto uscendo dai margini della telecamera
        {
            position.x = position.x + asseX * Time.deltaTime * speed;
            rb.MovePosition(position);
        }
        speed = startingSpeed * GameController.speedFactor;
    }

    // Update is called once per frame
    void Update()
    {
        asseX = Input.GetAxisRaw("Horizontal");  //leggo l'input
        if (Mathf.Approximately(asseX, 0.0f))
        {
            animator.SetBool("Stopped", true);
        }
        else
        {
            animator.SetBool("Stopped", false);
        }
        animator.SetFloat("MoveX", asseX);


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

        shieldActive = shield.activeSelf;
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
               
                hbarLow.SetActive(false);       //boh, funziona solo qui e non negli if di sopra
                gameOver.SetActive(true);      //gameover
                laMiaNave.SetActive(false);  //gameover
                partita = false;
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
