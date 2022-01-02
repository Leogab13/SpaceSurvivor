using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlanets : MonoBehaviour
{
    private float startingSpeed = 0.4f;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.partita == true)
        {

            if (transform.position.y > -5.0f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
            }
            else
            {
                this.gameObject.SetActive(false);
            }
            speed = startingSpeed * GameController.speedFactor;
        }
    }
}
