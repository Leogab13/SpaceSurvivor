using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGroup : MonoBehaviour
{
    public float velocita = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (transform.position.y > -20.0f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (velocita * Time.deltaTime));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
