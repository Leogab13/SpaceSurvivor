using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour    
{
    Rect left;
    Rect right;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        left = new Rect(0, 0, Screen.width / 2, Screen.height);
        right = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (left.Contains(touch.position))
            {
                Debug.Log("left touched");
            }
            if (right.Contains(touch.position))
            {
                Debug.Log("right touched");
            }
        }
    }
}
