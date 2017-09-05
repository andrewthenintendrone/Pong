using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Color[] colors = new Color[2];

    public int playerNumber = 1;
    public float movementSpeed = 15.0f;

	// Use this for initialization
	void Start()
    {
        // set colors
        GetComponent<Renderer>().material.SetColor("_Color1", colors[0]);
        GetComponent<Renderer>().material.SetColor("_Color2", colors[1]);
    }
	
	// Update is called once per frame
	void Update()
    {
        float movementDirection = Input.GetAxisRaw("Player" + playerNumber.ToString());
        transform.Translate(Vector3.up * movementDirection * movementSpeed * Time.deltaTime);

        if(transform.position.y >= 3.5f)
        {
            Vector3 tempTransform = transform.position;
            tempTransform.y = 3.5f;
            transform.position = tempTransform;
        }
        else if (transform.position.y <= -3.5f)
        {
            Vector3 tempTransform = transform.position;
            tempTransform.y = -3.5f;
            transform.position = tempTransform;
        }
    }
}
