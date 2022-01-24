using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    AudioSource audioSource;
    
    public GameObject[] asteroidGroups;
    private float asteroidTimer;
    private float asteroidStartingTimeToSpawn = 7.0f;
    private float asteroidTimeToSpawn;
    private Vector2 asteroidSpawnPosition;
    private Quaternion[] rotations;
    private Vector2 asteroidPoolPosition;
    private GameObject[] instantiatedAsteroidGroups;
    private int asteroidPoolSize;
    private int lastIndex;
    private int randomIndex;

    public GameObject[] planets;
    private float planetTimer;
    private float planetTimeToSpawn = 0.0f;
    private int planetsIndex = 0;
    private Vector2 planetPoolPosition;
    private Vector2 planetSpawnPosition;

    public GameObject explosion;
    public static GameObject[] explosions;
    private Vector2 explosionsPoolPosition;
    public static int explosionIndex;

    public float gameTime;
    public static float speedFactor;
    private float speedFactorMax = 4.0f;
    public float speedFactorTest; //DA RIMUOVERE

    private GameObject ship;

    public GameObject gameOver;         //la sprite della scritta GAME OVER
    public static bool partita;  //gestione della partita, true=vivo false=gameover
    private bool processedDeath;     //se ho già verificato la morte

   

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        asteroidTimer = 0.0f;
        asteroidTimeToSpawn = asteroidStartingTimeToSpawn;
        planetTimer = planetTimeToSpawn;
        gameTime = 0.0f;
        speedFactor = 1.0f;

        partita = true;
        processedDeath = false;

        rotations = new Quaternion[4];
        rotations[0] = Quaternion.identity;
        rotations[1] = Quaternion.AngleAxis(180, Vector3.up);
        rotations[2] = Quaternion.AngleAxis(180, Vector3.right);
        rotations[3] = Quaternion.AngleAxis(180, Vector3.forward);

        asteroidPoolPosition = new Vector2(-15.0f, -15.0f);
        asteroidPoolSize = asteroidGroups.Length * rotations.Length;
        instantiatedAsteroidGroups = new GameObject[asteroidPoolSize];
        int k = 0;
        for (int i = 0; i < asteroidGroups.Length; i++)
        {
            for (int j = 0; j < rotations.Length; j++)
            {
                instantiatedAsteroidGroups[k] = (GameObject)Instantiate(asteroidGroups[i], asteroidPoolPosition, rotations[j]);
                instantiatedAsteroidGroups[k].SetActive(false);
                k++;
            }
        }
        asteroidSpawnPosition = new Vector2(0.0f, 6.4f);

        planetPoolPosition = new Vector2(15.0f, -15.0f);
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i] = (GameObject)Instantiate(planets[i], planetPoolPosition, Quaternion.identity);
            planets[i].SetActive(false);
        }

        explosions = new GameObject[10];
        explosionIndex = 0;
        explosionsPoolPosition = new Vector2(20.0f, -15.0f);
        for (int i = 0; i < explosions.Length; i++)
        {
            explosions[i] = (GameObject)Instantiate(explosion, explosionsPoolPosition, Quaternion.identity);
            explosions[i].SetActive(false);
        }

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
            if (speedFactor<speedFactorMax)
            {
                speedFactor += 0.0003f;
                speedFactorTest = speedFactor; //DA RIMUOVERE
            }

            if (asteroidTimer > 0.0f)
            {
                asteroidTimer -= Time.deltaTime;
            }
            else
            {
                lastIndex = randomIndex;
                randomIndex = Random.Range(0, asteroidPoolSize);
                while (randomIndex == lastIndex)
                {
                    randomIndex = Random.Range(0, asteroidPoolSize);
                }
                instantiatedAsteroidGroups[randomIndex].transform.position = asteroidSpawnPosition;
                instantiatedAsteroidGroups[randomIndex].SetActive(true);
                instantiatedAsteroidGroups[randomIndex].GetComponent<AsteroidGroup>().Reset();
                asteroidTimer = asteroidTimeToSpawn;
            }
            asteroidTimeToSpawn = asteroidStartingTimeToSpawn / speedFactor;

            if (planetTimer > 0.0f)
            {
                planetTimer -= Time.deltaTime;
            }
            else
            {
                planetSpawnPosition = new Vector2(Random.Range(-1.7f, 1.7f), 4.0f);
                planets[planetsIndex].transform.position = planetSpawnPosition;
                planets[planetsIndex].SetActive(true);
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
