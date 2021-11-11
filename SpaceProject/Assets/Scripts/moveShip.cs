using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShip : MonoBehaviour
{
    Rigidbody2D rb;              //corpo nave
    public float speed = 100;    //velocità nave

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

  

    void FixedUpdate()      //con il fixed update controllo i movimenti della nave
    {

        if (transform.position.x >= -1.56 && transform.position.x <= 1.56)
        {

            float asseX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

            rb.velocity = new Vector2(asseX, 0);
        }

       

    }

    void Update()        //codice per limitare lo spostamento della nave al range della telecamera
    {
        if (transform.position.x < -1.56f)
        {
            transform.position = new Vector2(-1.55f, -2.5f);             //a little teleport (shh) #1
        }

        if (transform.position.x > 1.56f)
        {
            transform.position = new Vector2(1.55f, -2.5f);             //a little teleport (shh) #2
        }


    }


}
