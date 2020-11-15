using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleDeath : MonoBehaviour
{
    public int hitsToDeath;
    public GameObject green;
    public GameObject yellow;
    public GameObject orange;
    public GameObject red;
    public GameObject deathParticles;
    public GameObject oneHitPaddleParticles;
    public GameObject twoHitPaddleParticles;
    public GameObject threeHitPaddleParticles;
    public GameObject fourHitPaddleParticles;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // hit brick
        if (other.CompareTag("BrickFall"))
        {
            DeadPaddle();
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitsToDeath > 4) hitsToDeath = 4;
        transform.gameObject.tag = "paddle";
        if (hitsToDeath == 4)
        {
            deathParticles = fourHitPaddleParticles;
        }
        else if (hitsToDeath == 3)
        {
            if (green != null) green.SetActive(false);
            deathParticles = threeHitPaddleParticles;
        }
        else if (hitsToDeath == 2)
        {
            if (yellow != null) yellow.SetActive(false);
            deathParticles = twoHitPaddleParticles;
        }
        else if (hitsToDeath == 1)
        {
            if (orange != null) orange.SetActive(false);
            deathParticles = oneHitPaddleParticles;
        }
    }

    private void DeadPaddle()
    {
        //if (hitsToDeath == 4) GM.instance.fourHitBrickSound();
        //else if (hitsToDeath == 3) GM.instance.threeHitBrickSound();
        //else if (hitsToDeath == 2) GM.instance.twoHitBrickSound();
        //else if (hitsToDeath == 1) GM.instance.oneHitBrickSound();
        hitsToDeath--;
        Debug.Log("the paddle got hit");
        Instantiate(deathParticles, transform.position + (new Vector3(0f, 0f, -1f)), Quaternion.identity);
        if (hitsToDeath == 0)
        {
            GM.instance.DestroyedPaddle();
        }
    }
}
