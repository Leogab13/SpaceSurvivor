using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class GameController : MonoBehaviour
{
    public static GameController instance;

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

    //public float gameTime;
    public static float speedFactor;
    private float speedFactorMax = 4.0f;
    public float speedFactorTest; //DA RIMUOVERE

    private GameObject ship;

    /*public GameObject gameOver;         //la sprite della scritta GAME OVER                                       
    public GameObject RestartOver;      //la scritta Ricomincia
    public GameObject MenuOver;         //la scritta Menù
    public GameObject Pause;            //la sprite Pausa*/
    public static bool partita;  //gestione della partita, true=vivo false=gameover
    private bool processedDeath;     //se ho già verificato la morte

    public static int highScore;

    //Firebase variables
    [Header("Firebase")]
    public static FirebaseUser user;
    public static DatabaseReference DBreference;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeAsteroidsSpawn();
        InitializePlanetsSpawn();
        InitializeRotations();
        //gameTime = 0.0f;
        speedFactor = 1.0f;

        partita = true;
        processedDeath = false;

        InitializeAsteroidPool();
        InitializePlanetPool();
        InitializeExplosionPool();        

        ship = GameObject.Find("myShip");

        user = GameFirebaseManager.user;
        DBreference = GameFirebaseManager.DBreference;
        if (user != null)
        {
            StartCoroutine(LoadHighScore());
        }
        else highScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameOverFunction(); //gestione game over        
    }

    void FixedUpdate()
    {
        if (partita == true)  //gameover
        {
            //gameTime += Time.deltaTime;
            UpdateSpeedFactor();
            SpawnAsteroids();
            SpawnPlanets();
        }
    }

    private void InitializeAsteroidsSpawn()
    {
        asteroidTimer = 0.0f;
        asteroidTimeToSpawn = asteroidStartingTimeToSpawn;
    }
    private void InitializePlanetsSpawn()
    {
        planetTimer = planetTimeToSpawn;
    }
    private void InitializeRotations()
    {
        rotations = new Quaternion[4];
        rotations[0] = Quaternion.identity;
        rotations[1] = Quaternion.AngleAxis(180, Vector3.up);
        rotations[2] = Quaternion.AngleAxis(180, Vector3.right);
        rotations[3] = Quaternion.AngleAxis(180, Vector3.forward);
    }
    private void InitializeAsteroidPool()
    {
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
    }
    private void InitializePlanetPool()
    {
        planetPoolPosition = new Vector2(15.0f, -15.0f);
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i] = (GameObject)Instantiate(planets[i], planetPoolPosition, Quaternion.identity);
            planets[i].SetActive(false);
        }
    }
    private void InitializeExplosionPool()
    {
        explosions = new GameObject[10];
        explosionIndex = 0;
        explosionsPoolPosition = new Vector2(20.0f, -15.0f);
        for (int i = 0; i < explosions.Length; i++)
        {
            explosions[i] = (GameObject)Instantiate(explosion, explosionsPoolPosition, Quaternion.identity);
            explosions[i].SetActive(false);
        }
    }

    private void GameOverFunction() //gestione game over
    {
        if (ShipController.dead && !processedDeath)
        {
            /*gameOver.SetActive(true);      //gameover
            RestartOver.SetActive(true);   //Ricomincia
            MenuOver.SetActive(true);      //Menù
            Pause.SetActive(false);        //Pausa*/
            UIManagerGame.instance.GameOverScreen();
            ship.gameObject.SetActive(false); //faccio scomparire la nave
            partita = false;                  //fermo tutti gli oggetti mobili
            audioSource.Play();

            processedDeath = true;
            if (user != null)
            {
                int newScore = Punteggio.score;
                if (newScore > highScore)
                {
                    StartCoroutine(UpdateHighScore(newScore));
                }
            }               
        }
    }
    private void UpdateSpeedFactor()
    {
        if (speedFactor < speedFactorMax)
        {
            speedFactor += 0.0003f;
            speedFactorTest = speedFactor; //DA RIMUOVERE
        }
    }
    private void SpawnAsteroids()
    {
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
    }
    private void SpawnPlanets()
    {
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
    private IEnumerator LoadHighScore()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            highScore = 0;
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            bool hasScore = snapshot.HasChild("score");
            if (!hasScore)
            {
                //no score exists yet
                highScore = 0;
            }
            else
            {
                if (snapshot.Child("score").Value != null)
                {
                    string score = (string)snapshot.Child("score").Value.ToString();
                    highScore = int.Parse(score);
                }
                //highScore = (int)snapshot.Child("score").Value;
            }
        }        
    }
    private IEnumerator UpdateHighScore(int _newScore)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("score").SetValueAsync(_newScore);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Score is now updated
        }
    }
}
