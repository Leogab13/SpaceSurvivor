using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    Rigidbody2D rb;              //corpo nave
    public float startingSpeed = 3.0f;// velocit� di partenza nave
    public float speed;    //velocit� nave
    Animator animator;
    float asseX;
    public GameObject explosion;
    public GameObject gameOver;
    public GameObject laMiaNave;
    
    //di seguito le 3 barre salute
    public GameObject hbarHigh;
    public GameObject hbarMedium;
    public GameObject hbarLow;


    public int life = 3;   // 3 vite a partita , viene decrementato di 1 ad ogni collisione , a 0=gameover , uso life anche per gestire la healthbar
    public static bool partita=true;      //gestione della partita, true=vivo false=gameover



    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = startingSpeed;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)   //gestione collisioni
    {

        if (collision.name.Contains("Coin"))
        {
            collision.gameObject.SetActive(false);
            punteggio.score = punteggio.score + 2000;
        }
        if (collision.name.Contains("Asteroid"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
            gameOver.SetActive(true);      //gameover
            laMiaNave.SetActive(false);  //gameover
            partita = false;     //gameover
            life = life - 1;    //decremento di 1 la vita
        }

    }

   

}
