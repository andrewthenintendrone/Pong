using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    // variables
    [SerializeField]
    private float baseSpeed = 5.0f; // speed the ball starts at
    [SerializeField]
    private float currentSpeed; // speed the ball is currently moving at
    [SerializeField]
    private float accelerationRate = 0.1f; // how fast the ball accelerates over time
    [SerializeField]
    private float spinSpeed = 5.0f; // how fast the ball should spin while moving
    [SerializeField]
    private int[] scores = new int[2]; // ball keeps track of both players scores

    // components
    private Rigidbody rb; // reference to rigidbody
    private TrailRenderer trail; // reference to trail renderer
    private Renderer rend; // reference to renderer
    private Text scoreboard; // reference to the scoreboard


	void Start ()
    {
        // find components (saves having to access each frame which is expensive)
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        rend = GetComponent<Renderer>();
        scoreboard = GameObject.Find("scoreboard").GetComponent<Text>();

        // reset and launch the ball after 1 second
        resetBall();
        StartCoroutine(LaunchBall(1.0f));
    }

    // called when this object enters a trigger
    void OnTriggerEnter(Collider other)
    {
        // if the trigger is tagged as a respawn
        if (other.gameObject.tag == "Respawn")
        {
            // if the ball is on the right increase player 1s score
            // if the ball is on the left increase player 2s score
            scores[transform.position.x > 0 ? 0 : 1]++;

            // update the scoreboard to reflect the new scores
            scoreboard.text = scores[0] + " : " + scores[1];

            // reset the ball and launch it again
            resetBall();
            StartCoroutine(LaunchBall(0.25f));
        }
    }

    void Update()
    {
        // each second the current speed increase by the acceleration rate
        currentSpeed += accelerationRate * Time.deltaTime;

        // manually set the balls velocity to avoid it from stopping
        rb.velocity = rb.velocity.normalized * currentSpeed;
    }

    // resets the ball's position and velocity and randomizes it's colors
    void resetBall()
    {
        // set the first color to a random color with a bias towards being bright (values more than 0.5)
        rend.material.SetColor("_Color1", new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)));
        // set the second color to a random color with a bias towards being dark (values less than 0.5)
        rend.material.SetColor("_Color2", new Color(Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f), Random.Range(0.0f, 0.5f)));

        // reset velocity and torque to "freeze the ball"
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // clear the trail
        trail.Clear();

        // reset position and rotation to defaults
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        // reset current speed to base speed
        currentSpeed = baseSpeed;
    }

    // launches the ball after a certain number of sceonds
    IEnumerator LaunchBall(float seconds)
    {
        // wait for the number of seconds to pass
        yield return new WaitForSeconds(seconds);

        // pick a random angle between -30 and 30
        float randomAngle = Random.Range(-30.0f, 30.0f);
        // there is a 50% change to add 180 degrees to the angle
        randomAngle += Random.value < 0.5f ? 180.0f : 0.0f;
        
        // apply the randomized rotation
        rb.transform.rotation = (Quaternion.Euler(new Vector3(0.0f, 0.0f, randomAngle)));
        
        // add force in that direction
        rb.AddForce(transform.right * baseSpeed, ForceMode.Impulse);
        
        // choose a random direction to spin in
        Vector3 spin = Random.insideUnitCircle.normalized * spinSpeed;
        
        // add torque in that direction
        rb.AddTorque(spin, ForceMode.Impulse);
    }
}
