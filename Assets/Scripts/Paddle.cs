using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Color[] colors = new Color[2];
    public int playerNumber = 1;
    public float movementSpeed = 500.0f;

	// Use this for initialization
	void Start()
    {
        // set colors
        GetComponent<Renderer>().material.SetColor("_Color1", colors[0]);
        GetComponent<Renderer>().material.SetColor("_Color2", colors[1]);
    }
	
	void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float movementDirection = 0.0f;

        // windows uses arrow keys and ws
        // mac and linux?
#if UNITY_STANDALONE_WIN
        movementDirection = Input.GetAxisRaw("Player" + playerNumber + "Movement");
#endif

        // editor uses standard windows controls and fake touch system
#if UNITY_EDITOR
        movementDirection = Input.GetAxisRaw("Player" + playerNumber + "Movement");

        bool click = Input.GetMouseButton(0);

        if (click)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 tempPos = transform.position;

            if (playerNumber == 1)
            {
                if(mousePos.x < Screen.width / 3)
                {
                    tempPos.y = screenMousePos.y;
                }
            }
            else
            {
                if(mousePos.x > Screen.width * 2 / 3)
                {
                    tempPos.y = screenMousePos.y;
                }
            }
            transform.position = tempPos;
        }
#endif

        // android uses touch system
#if UNITY_ANDROID

        Touch[] touches = Input.touches;

        if(touches.Length > 0)
        {
            foreach (Touch currentTouch in touches)
            {
                Vector2 mousePos = currentTouch.position;
                Vector2 screenMousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 tempPos = transform.position;

                if (playerNumber == 1)
                {
                    if (mousePos.x < Screen.width / 3)
                    {
                        tempPos.y = screenMousePos.y;
                    }
                }
                else
                {
                    if (mousePos.x > Screen.width * 2 / 3)
                    {
                        tempPos.y = screenMousePos.y;
                    }
                }
                transform.position = tempPos;
            }
        }
#endif

        transform.Translate(Vector3.up * movementDirection * movementSpeed * Time.deltaTime);

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
