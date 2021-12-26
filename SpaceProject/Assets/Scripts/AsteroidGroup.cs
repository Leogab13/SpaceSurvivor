using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGroup : MonoBehaviour
{
    private float startingSpeed = 1.0f;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    void FixedUpdate()
    {
        if (transform.position.y > -6.0f)
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
