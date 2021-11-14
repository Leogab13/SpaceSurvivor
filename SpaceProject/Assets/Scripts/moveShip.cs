using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShip : MonoBehaviour
{
    Rigidbody2D rb;              //corpo nave
    public float speed = 3.0f;    //velocità nave
    Animator animator;
    float asseX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

     void FixedUpdate()      //con il fixed update controllo i movimenti della nave
    {
        Vector2 position = rb.position;

        if (!(position.x <= -1.56 && asseX < 0)&&!(position.x >= 1.56 && asseX > 0))//se non sto uscendo dai margini della telecamera
        {
            position.x = position.x + asseX * Time.deltaTime * speed;
            rb.MovePosition(position);
        }



    }

    // Update is called once per frame
    void Update()        
    {
        asseX = Input.GetAxisRaw("Horizontal"); //leggo l'input        
    }


}
