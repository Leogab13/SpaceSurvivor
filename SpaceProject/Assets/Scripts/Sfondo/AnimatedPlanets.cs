using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlanets : MonoBehaviour
{
    public float velocita = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > -5.0f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (velocita * Time.deltaTime));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
