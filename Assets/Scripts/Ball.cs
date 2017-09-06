﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    private float currentSpeed;
    public float accelerationRate = 0.1f;
    public float spinSpeed = 5.0f;

    private Rigidbody rb;
    private TrailRenderer trail;
    private Renderer rend;

	// Use this for initialization
	void Start ()
    {
        // store components
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        rend = GetComponent<Renderer>();
        // reset and launch the ball after 1 second
        resetBall();
        StartCoroutine(LaunchBall(1.0f));
    }

    // triggered when this object enters a trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            resetBall();
            StartCoroutine(LaunchBall(0.25f));
        }
    }

    // triggered when this object touches a collider
    void OnCollisionEnter(Collision collision)
    {
        
    }

    void FixedUpdate()
    {
        if(rb.velocity.sqrMagnitude < baseSpeed)
        {
            rb.velocity = rb.velocity.normalized * baseSpeed;
        }
    }

    // resets the ball's position and velocity
    void resetBall()
    {
        // rerandomize colors
        rend.material.SetColor("_Color1", new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)));
        rend.material.SetColor("_Color2", new Color(Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f)));

        // kill momentum
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // erase trail
        trail.Clear();

        // reset position to center
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    // launches the ball after a certain number of sceonds
    IEnumerator LaunchBall(float seconds)
    {
        // wait for the number of seconds to pass
        yield return new WaitForSeconds(seconds);
        // pick a random angle between -30 and 30 or 150 and 210 (60 degrees)
        float randomAngle = Random.Range(-30.0f, 30.0f) + (Random.value < 0.5f ? 180.0f : 0.0f);
        // apply the random rotation
        rb.transform.rotation = (Quaternion.Euler(new Vector3(0.0f, 0.0f, randomAngle)));
        // add force in that direction
        rb.AddForce(transform.right * baseSpeed, ForceMode.Impulse);
        // choose a random direction to spin in
        Vector3 spin = Random.insideUnitCircle.normalized * spinSpeed;
        // add torque in that direction
        rb.AddTorque(spin, ForceMode.Impulse);
    }
}
