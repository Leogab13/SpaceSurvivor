using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoNave : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float asseX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        rb.velocity = new Vector2(asseX,0);

    }
}
