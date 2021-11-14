using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] planets;
    private float timer;
    private float timeToSpawn = 0.0f;
    private int planetsIndex = 0;
    public float GameTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToSpawn;
        GameTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.deltaTime;

        if (timer > 0.0f)
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
