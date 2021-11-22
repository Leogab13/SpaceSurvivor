using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] asteroids;
    private float asteroidTimer;
    private float asteroidTimeToSpawn = 14.0f;
    private Quaternion[] rotations;

    public GameObject[] planets;
    private float planetTimer;
    private float planetTimeToSpawn = 0.0f;
    private int planetsIndex = 0;

    public float GameTime;

    // Start is called before the first frame update
    void Start()
    {
        asteroidTimer = 0.0f;
        planetTimer = planetTimeToSpawn;
        GameTime = 0.0f;
        rotations = new Quaternion[4];
        rotations[0] = Quaternion.identity;
        rotations[1] = Quaternion.AngleAxis(180, Vector3.up);
        rotations[2] = Quaternion.AngleAxis(180, Vector3.right);
        rotations[3] = Quaternion.AngleAxis(180, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.deltaTime;

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
