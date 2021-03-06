using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNebulae : MonoBehaviour
{
    private float startingSpeed = 0.12f;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startingSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.partita == true)   //gameover
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
