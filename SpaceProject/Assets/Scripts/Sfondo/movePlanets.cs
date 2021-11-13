using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlanets : MonoBehaviour
{
    public float velocita = 0.2f;
    public GameObject[] planets;
    private float timer;
    private float timeToSpawn = 0.0f;
    private int planetsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToSpawn;
    }

    void FixedUpdate()
    {
        if (transform.position.y > -12.8f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (velocita * Time.deltaTime));
        }
        else
        {
            transform.position = new Vector2(0f, 25.6f);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            var position = new Vector2(Random.Range(-1.7f, 1.7f), 4.0f);
            Instantiate(planets[planetsIndex], position, Quaternion.identity);
            planetsIndex++;
            timeToSpawn += 20.0f;
            if (planetsIndex >= planets.Length)
            {
                planetsIndex = 0;
                timeToSpawn = 0.0f;
            }
            timer = timeToSpawn;
        }
    }
}
