using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static float totalTime = 13.0f;
    public static float timer;
    private GameObject shieldSFX;

    private Vector2 shipPosition;

    // Start is called before the first frame update
    void Start()
    {
        timer = totalTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            timer = totalTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Asteroid"))
        {
            shipPosition = this.transform.position;
            GameController.explosions[GameController.explosionIndex].transform.position = shipPosition;
            GameController.explosions[GameController.explosionIndex].SetActive(true);
            GameController.explosionIndex++;
            if (GameController.explosionIndex >= GameController.explosions.Length)
            {
                GameController.explosionIndex = 0;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
