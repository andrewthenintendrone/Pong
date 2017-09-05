using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float baseMovementSpeed = 5.0f;
    public float movementSpeed;
    public Vector3 direction;
    private Rigidbody rb;
    private TrailRenderer trail;
    private Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        rend = GetComponent<Renderer>();
        resetBall();
        StartCoroutine(LaunchBall(2.0f));
    }

    void FixedUpdate()
    {
        movementSpeed += 0.01f;
        rb.velocity = rb.velocity.normalized * movementSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("respawn"))
        {
            resetBall();
            StartCoroutine(LaunchBall(0.5f));
        }
    }

    // resets the ball's position and velocity
    void resetBall()
    {
        // randomize colors
        rend.material.SetColor("_Color1", new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)));
        rend.material.SetColor("_Color2", new Color(Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f)));

        trail.Clear();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        movementSpeed = baseMovementSpeed;
    }

    IEnumerator LaunchBall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        float randomAngle = Random.Range(-30.0f, 30.0f) + (Random.value < 0.5f ? 180.0f : 0.0f);
        transform.Rotate(new Vector3(0.0f, 0.0f, randomAngle));
        rb.AddForce(transform.right * baseMovementSpeed, ForceMode.VelocityChange);
        rb.AddTorque(transform.right * baseMovementSpeed, ForceMode.VelocityChange);
    }
}
