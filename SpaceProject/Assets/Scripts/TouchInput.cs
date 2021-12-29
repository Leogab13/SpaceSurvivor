using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour    
{
    private Rect left;
    private Rect right;
    private Touch touch;
    public static bool touched;
    public static float axis;

    // Start is called before the first frame update
    void Start()
    {
        left = new Rect(0, 0, Screen.width / 2, Screen.height);
        right = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
        touched = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touched = true;
            touch = Input.GetTouch(0);

            if (left.Contains(touch.position))
            {
                axis = -1;
            }
            if (right.Contains(touch.position))
            {
                axis = 1;
            }
        }
        else
        {
            axis = 0;
        }
    }
}
