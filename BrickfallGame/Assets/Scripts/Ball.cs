using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance = null;

    float ballInitialVelocity;
    float velocityX;
    float velocityY;
    float randomX;
    float randomY;
    float speed;
    public GameObject paddle;
    private Rigidbody rb;
    public bool ballInPlay;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        GM.instance.Bounce();

        if (ballInPlay == true)
        {
            if (rb.velocity.x > -1 && rb.velocity.x < 1)
            {
                if (gameObject.transform.position.x > 0)
                {
                    randomX = Random.Range(-60.0f, -20.0f);
                    rb.AddForce(new Vector3(randomX, 0, 0));
                }
                else if (gameObject.transform.position.x <= 0)
                {
                    randomX = Random.Range(20.0f, 60.1f);
                    rb.AddForce(new Vector3(randomX, 0, 0));
                }
            }
            if (rb.velocity.y > -1 && rb.velocity.y < 1)
            {
                if (gameObject.transform.position.y > 0)
                {
                    randomY = Random.Range(-60.0f, -20.0f);
                    rb.AddForce(new Vector3(0, randomY, 0));
                }
                else if (gameObject.transform.position.y <= 0)
                {
                    randomY = Random.Range(20.0f, 60.1f);
                    rb.AddForce(new Vector3(0, randomY, 0));
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = GM.instance.ballSpeed;

        rb.velocity = (rb.velocity.normalized * speed);

        if (ballInPlay == false)
        {
            paddle = GM.instance.clonePaddle;

            Vector3 paddlePosition = paddle.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
            transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                randomX = Random.Range(-30.0f, 31.0f);
                randomY = Random.Range(20.0f, 30.0f);
                transform.parent = null;
                ballInPlay = true;
                rb.isKinematic = false;
                rb.AddForce(new Vector3(randomX, randomY, 0));
            }
        }
    }
}