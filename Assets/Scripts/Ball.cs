using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 direction;
    private Rigidbody rb;
    private TrailRenderer trail;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        resetBall();
        StartCoroutine(LaunchBall(2.0f));
        // set random colors
        Color color1 = new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f));
        Color color2 = new Color(1.0f - color1.r, 1.0f - color1.g, 1.0f - color1.b);
        GetComponent<Renderer>().material.SetColor("_Color1", color1);
        GetComponent<Renderer>().material.SetColor("_Color2", color2);

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
        trail.Clear();
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    IEnumerator LaunchBall(float seconds)
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(seconds);
        direction = Random.insideUnitCircle.normalized;
        rb.AddForce(direction * movementSpeed, ForceMode.Impulse);
        rb.AddTorque(direction * movementSpeed, ForceMode.Impulse);
    }
}
