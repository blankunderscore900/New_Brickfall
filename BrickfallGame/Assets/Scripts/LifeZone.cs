using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeZone : MonoBehaviour
{
    public int ballCount;
    public bool gameStart;
    public int bricks;
    public GameObject LZ;

    void Awake()
    {
        ballCount = 0;
        gameStart = false;
        LZ.gameObject.transform.localScale = new Vector3(8, 10, 11);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameStart = true;
            LZ.gameObject.transform.localScale = new Vector3(9, 10, 11);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            LZ.gameObject.transform.localScale = new Vector3(8, 10, 11);
        }
    }

    void Update()
    {
        bricks = GM.instance.amountOfBricksInLevel;
        ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;

        if (ballCount <= 0 && gameStart == true && GM.instance.youWin == false)
        {
            gameStart = false;
            GM.instance.LoseLife();
        }




    }
}
