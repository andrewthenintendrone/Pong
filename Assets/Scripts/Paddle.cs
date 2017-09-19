using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // each paddle has 2 preset colors
    public Color[] colors = new Color[2];

    // player 1 or 2
    public int playerNumber = 1;

    // speed to move at
    public float movementSpeed = 500.0f;


	void Start()
    {
        // set material colors to presets
        GetComponent<Renderer>().material.SetColor("_Color1", colors[0]);
        GetComponent<Renderer>().material.SetColor("_Color2", colors[1]);
    }
	
	void Update()
    {
        // handle player input
        HandleInput();
    }

    // handles cross platform player inputs
    void HandleInput()
    {
        // desired y axis movement direction
        float movementDirection = 0.0f;

        // windows
#if UNITY_STANDALONE_WIN
        // get input from an axis based on the player number
        movementDirection = Input.GetAxisRaw("Player" + playerNumber + "Movement");

        // move based on desired movement direction
        transform.Translate(Vector3.up * movementDirection * movementSpeed * Time.deltaTime);
#endif

        // android uses a touch system
#if UNITY_ANDROID

        // store an array of screen touches
        Touch[] touches = Input.touches;

        // don't do any more checks if there are currently no touches
        if(touches.Length > 0)
        {
            // store our current position as tempPos
            Vector3 tempPos = transform.position;

            foreach (Touch currentTouch in touches)
            {
                // translate each touch into world space
                Vector2 touchPos = currentTouch.position;
                Vector2 screenTouchPos = Camera.main.ScreenToWorldPoint(touchPos);

                // player 1 uses the left part of the screen
                if (playerNumber == 1)
                {
                    if (touchPos.x < Screen.width / 3)
                    {
                        // set the y position of tempPos to the touch point's y position
                        tempPos.y = screenTouchPos.y;
                    }
                }
                // player 2 uses the right part of the screen
                else if(playerNumber == 2)
                {
                    if (touchPos.x > Screen.width * 2 / 3)
                    {
                        // set the y position of tempPos to the touch point's y position
                        tempPos.y = screenTouchPos.y;
                    }
                }

                // set our position to the modified tempPos
                transform.position = tempPos;
            }
        }
#endif

        // the editor combines the windows and android control schemes
#if UNITY_EDITOR
        // get input from an axis based on the player number
        movementDirection = Input.GetAxisRaw("Player" + playerNumber + "Movement");

        // simulate android touch screen using a click
        bool click = Input.GetMouseButton(0);

        // don't do any more checks if there is no click
        if (click)
        {
            // store our current position as tempPos
            Vector3 tempPos = transform.position;

            // translate the click into world space
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // player 1 uses the left part of the screen
            if (playerNumber == 1)
            {
                if(mousePos.x < Screen.width / 3)
                {
                    // set the y position of tempPos to the click's y position
                    tempPos.y = screenMousePos.y;
                }
            }
            // player 2 uses the right part of the screen
            else if(playerNumber == 2)
            {
                if(mousePos.x > Screen.width * 2 / 3)
                {
                    // set the y position of tempPos to the click's y position
                    tempPos.y = screenMousePos.y;
                }
            }

            // set our position to the modified tempPos
            transform.position = tempPos;

            // move based on desired movement direction
            transform.Translate(Vector3.up * movementDirection * movementSpeed * Time.deltaTime);
        }
#endif

        // keep the paddles within the play field 
        if (transform.position.y > 3.5f)
        {
            transform.position += Vector3.down * (transform.position.y - 3.5f);
        }
        else if (transform.position.y < -3.5f)
        {
            transform.position += Vector3.up * (-transform.position.y - 3.5f);
        }
    }
}
