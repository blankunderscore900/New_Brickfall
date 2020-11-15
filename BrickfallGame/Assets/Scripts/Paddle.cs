using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;


    private Vector3 playerPos = new Vector3(0, -3.6f, 0);
    private float Range1 = -3.6f;
    private float Range2 = 3.6f;

    // Update is called once per frame
    void Update()
    {
        speed = GM.instance.paddleSpeed;
        float xPos = transform.position.x + (Input.GetAxis("Mouse X") * speed);
        playerPos = new Vector3(Mathf.Clamp(xPos, Range1, Range2), -3.6f, 0f);
        transform.position = playerPos;
    }

    void normalPaddleRange()
    {
        Range1 = -3.6f;
        Range2 = 3.6f;
    }

    void bigPaddleRange()
    {
        Range1 = -2.6f;
        Range2 = 2.6f;
    }

    void smallPaddleRange()
    {
        Range1 = -4.1f;
        Range2 = 4.1f;
    }


    // for power ups
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExtraLife"))
        {
            GM.instance.GetLife();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EnlargenPaddle"))
        {
            GM.instance.PaddleSizeUp();
            bigPaddleRange();
            Invoke("normalPaddleRange", 5.0f);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("ShrinkPaddle"))
        {
            GM.instance.PaddleSizeDown();
            smallPaddleRange();
            Invoke("normalPaddleRange", 5.0f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("FastBall"))
        {
            GM.instance.FastBall();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SlowBall"))
        {
            GM.instance.SlowBall();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SpawnBall"))
        {
            GM.instance.SpawnBall();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bomb"))
        {
            GM.instance.BlowUp();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("ScoreX2"))
        {
            GM.instance.DoublePoints();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CamFlip"))
        {
            GM.instance.CamFlip();
            Destroy(other.gameObject);
        }
    }
}
