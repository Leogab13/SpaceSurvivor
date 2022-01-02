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
        if (transform.position.y > -6.4f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        speed = startingSpeed * GameController.speedFactor;
    }

      public void Reset()
      {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
       }
}
