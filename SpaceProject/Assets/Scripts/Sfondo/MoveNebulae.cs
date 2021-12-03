using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNebulae : MonoBehaviour
{
    public float startingSpeed = 0.06f;
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
