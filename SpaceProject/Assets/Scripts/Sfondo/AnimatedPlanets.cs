using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlanets : MonoBehaviour
{
    public float startingSpeed = 0.2f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShipController.partita == true)
        {

            if (transform.position.y > -5.0f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
            }
            else
            {
                Destroy(gameObject);
            }
            speed = startingSpeed * GameController.speedFactor;
        }
    }
}
