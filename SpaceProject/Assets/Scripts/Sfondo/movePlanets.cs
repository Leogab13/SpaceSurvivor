using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlanets : MonoBehaviour
{
    public float startingSpeed = 0.2f;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    void FixedUpdate()
    {
        if (GameController.partita == true)
        {

            if (transform.position.y > -12.8f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
            }
            else
            {
                transform.position = new Vector2(0f, 25.6f);
            }
            speed = startingSpeed * GameController.speedFactor;
        }
    }
}
