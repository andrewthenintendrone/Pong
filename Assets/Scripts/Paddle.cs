using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Color[] colors = new Color[2];
    public int playerNumber = 1;
    public float movementSpeed = 500.0f;
    public float rotationSpeed = 720.0f;

    private Rigidbody rb;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        // set colors
        GetComponent<Renderer>().material.SetColor("_Color1", colors[0]);
        GetComponent<Renderer>().material.SetColor("_Color2", colors[1]);
    }
	
	void FixedUpdate()
    {
        float movementDirection = Input.GetAxisRaw("Player" + playerNumber + "Movement");
        rb.velocity = (Vector3.up * movementDirection * movementSpeed * Time.fixedDeltaTime);

        if(transform.position.y > 3.5f)
        {
            transform.position += Vector3.down * (transform.position.y - 3.5f);
        }
        else if(transform.position.y < -3.5f)
        {
            transform.position += Vector3.up * (-transform.position.y - 3.5f);
        }

        float rotateDirection = Input.GetAxisRaw("Player" + playerNumber + "Rotation");
        rb.angularVelocity = (Vector3.back * rotateDirection * rotationSpeed * Time.fixedDeltaTime);
    }
}
