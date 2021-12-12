using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    AudioSource audioSource;
    
    public GameObject[] asteroids;
    private float asteroidTimer;
    private float asteroidStartingTimeToSpawn = 14.0f;
    private float asteroidTimeToSpawn;
    private Quaternion[] rotations;

    public GameObject[] planets;
    private float planetTimer;
    private float planetTimeToSpawn = 0.0f;
    private int planetsIndex = 0;

    public float gameTime;
    public static float speedFactor;

    private GameObject ship;

    public GameObject gameOver;         //la sprite della scritta GAME OVER
    public static bool partita = true;  //gestione della partita, true=vivo false=gameover
    private bool processedDeath = false;     //se ho già verificato la morte

   

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        asteroidTimer = 0.0f;
        asteroidTimeToSpawn = asteroidStartingTimeToSpawn;
        planetTimer = planetTimeToSpawn;
        gameTime = 0.0f;
        speedFactor = 1.0f;

        rotations = new Quaternion[4];
        rotations[0] = Quaternion.identity;
        rotations[1] = Quaternion.AngleAxis(180, Vector3.up);
        rotations[2] = Quaternion.AngleAxis(180, Vector3.right);
        rotations[3] = Quaternion.AngleAxis(180, Vector3.forward);

        ship = GameObject.Find("myShip");
    }

    // Update is called once per frame
    void Update()
    {
        //gestione game over
        if (ShipController.dead && !processedDeath)
        {
            gameOver.SetActive(true);      //gameover

            ship.gameObject.SetActive(false); //faccio scomparire la nave
            partita = false;                  //fermo tutti gli oggetti mobili
            audioSource.Play();

            processedDeath = true;
        }       
    }

    void FixedUpdate()
    {
        if (partita == true)  //gameover
        {

            gameTime += Time.deltaTime;
            speedFactor += 0.0001f;


            if (asteroidTimer > 0.0f)
            {
                asteroidTimer -= Time.deltaTime;
            }
            else
            {
                var position = new Vector2(0.0f, 6.4f);
                Instantiate(asteroids[Random.Range(0, asteroids.Length)], position, rotations[Random.Range(0, rotations.Length)]);
                asteroidTimer = asteroidTimeToSpawn;
            }
            asteroidTimeToSpawn = asteroidStartingTimeToSpawn / speedFactor;

            if (planetTimer > 0.0f)
            {
                planetTimer -= Time.deltaTime;
            }
            else
            {
                var position = new Vector2(Random.Range(-1.7f, 1.7f), 4.0f);
                Instantiate(planets[planetsIndex], position, Quaternion.identity);
                planetsIndex++;
                planetTimeToSpawn += 20.0f;
                if (planetsIndex >= planets.Length)
                {
                    planetsIndex = 0;
                    planetTimeToSpawn = 0.0f;
                }
                planetTimer = planetTimeToSpawn;
            }
        }
    }
}
