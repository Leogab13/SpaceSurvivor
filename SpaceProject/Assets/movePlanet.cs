using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlanet : MonoBehaviour
{
    public float velocita = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
    }
}
