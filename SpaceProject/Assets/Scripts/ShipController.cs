using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    Rigidbody2D rb;              //corpo nave
    public float startingSpeed = 3.0f;// velocità di partenza nave
    public float speed;    //velocità nave
    Animator animator;
    float asseX;
    public GameObject explosion;
    public GameObject gameOver;
    public GameObject laMiaNave;
    public GameObject hBar3;

    public static bool partita=true;      //gestione della partita, true=vivo false=gameover



    // Start is called before the first frame update
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

    private void OnTriggerEnter2D(Collider2D collision)
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
            hBar3.SetActive(false);
            gameOver.SetActive(true);      //gameover
            laMiaNave.SetActive(false);  //gameover
            partita = false;     //gameover
        }

    }

   

}
