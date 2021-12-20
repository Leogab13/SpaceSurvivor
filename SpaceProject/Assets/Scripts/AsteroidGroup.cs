using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGroup : MonoBehaviour
{
    public float startingSpeed = 2.5f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    void FixedUpdate()
    {
        if (transform.position.y > -10.0f)
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
