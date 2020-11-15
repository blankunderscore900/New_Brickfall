using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // brick colors
    //public Color currentColor; // current color shows current durability of brick
    //public Color green; // brick is colored green (will be destroyed in four hits)
    //public Color yellow; // brick is colored yellow (will be destroyed in three hits)
    //public Color orange; // brick is colored orange (will be destroyed in two hits)
    //public Color red; // brick is colored red (will be destroyed in one hit)
    //public Color blue; // brick is colored blue (cannot be destroyed)
    public GameObject green;
    public GameObject yellow;
    public GameObject orange;
    public GameObject red;
    public GameObject blue;
    public bool indestructable; // brick is indestructible
    public int hitsToBreak;
    
    // brick particles
    public GameObject oneHitBrickParticles;
    public GameObject twoHitBrickParticles;
    public GameObject threeHitBrickParticles;
    public GameObject fourHitBrickParticles;
    public GameObject indestructableBrickParticles;
    private GameObject brickParticles;

    // powerups & debris
    public Transform powerup1;
    public Transform powerup2;
    public Transform powerup3;
    public Transform powerup4;
    public Transform powerup5;
    public Transform powerup6;
    public Transform powerup7;
    public Transform powerup8;
    public Transform powerup9;
    Transform chosenPowerUp;
    public Transform purpleDebris;

    //// sounds
    //public AudioSource oneHitBrick;
    //public AudioSource twoHitBrick;
    //public AudioSource threeHitBrick;
    //public AudioSource fourHitBrick;
    //public AudioSource indestructibleHitBrick;
    
    // points
    public int points;

    void Awake()
    {
        green.SetActive(false);
        yellow.SetActive(false);
        orange.SetActive(false);
        red.SetActive(false);
        blue.SetActive(false);

        //oneHitBrick = GM.instance.oneHitBrick;
        //twoHitBrick = GM.instance.twoHitBrick;
        //threeHitBrick = GM.instance.threeHitBrick;
        //fourHitBrick = GM.instance.fourHitBrick;
        //indestructibleHitBrick = GM.instance.indestructibleHitBrick;

        //oneHitBrick = GetComponent<AudioSource>();
        //twoHitBrick = GetComponent<AudioSource>();
        //threeHitBrick = GetComponent<AudioSource>();
        //fourHitBrick = GetComponent<AudioSource>();
        //indestructibleHitBrick = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        // hit brick
        Instantiate(brickParticles, transform.position + (new Vector3(0f, 0f, -1f)), Quaternion.identity);
        Invoke("HittingBrick", 0.075f);
    }

    void HittingBrick()
    {
        points = GM.instance.brickpoints;
        if (indestructable == false)
        {

            if (hitsToBreak == 4) GM.instance.fourHitBrickSound();
            if (hitsToBreak == 3) GM.instance.threeHitBrickSound();
            else if (hitsToBreak == 2) GM.instance.twoHitBrickSound();
            else if (hitsToBreak == 1) GM.instance.oneHitBrickSound();

            hitsToBreak--;

            if (hitsToBreak == 0)
            {
                Destroy(gameObject);

                float randChance = Random.Range(1, 101); // random chance of powerup showing
                float randPowerUp = Random.Range(1, 101);
                // first powerup
                if (randChance < 100) // 10% chance of powerup
                {
                    if (randPowerUp == 1) chosenPowerUp = powerup8; // 1% chance of selecting bomb as powerup
                    else if (randPowerUp == 2) chosenPowerUp = powerup1; // 1% chance
                    else if (randPowerUp == 3) chosenPowerUp = powerup6; // 1% chance
                    else if (randPowerUp > 3 && randPowerUp <= 5) chosenPowerUp = powerup4; // 2% chance
                    else if (randPowerUp > 5 && randPowerUp <= 19) chosenPowerUp = powerup3; // 14% chance
                    else if (randPowerUp > 19 && randPowerUp <= 33) chosenPowerUp = powerup9; // 14% chance
                    else if (randPowerUp > 33 && randPowerUp <= 47) chosenPowerUp = powerup5; // 14% chance
                    else if (randPowerUp > 47 && randPowerUp <= 61) chosenPowerUp = powerup2; // 14% chance
                    else if (randPowerUp > 61 && randPowerUp <= 75) chosenPowerUp = powerup7; // 14% chance
                    else if (randPowerUp > 75 && randPowerUp <= 100) chosenPowerUp = purpleDebris; // 25% chance


                    Instantiate(chosenPowerUp, transform.position, transform.rotation);
                }
                GM.instance.LowerBrickAmount();
            }

            GM.instance.Scoring(points);
        }
        else
        {
            GM.instance.indestructibleBrickSound();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (hitsToBreak > 4) hitsToBreak = 4;

        switch (indestructable)
        {
            case true:
                transform.gameObject.tag = "Untagged";
                brickParticles = indestructableBrickParticles;
                blue.SetActive(true);
                GM.instance.checkBrickGroup();
                break;
            case false:
                transform.gameObject.tag = "Brick";
                switch (hitsToBreak)
                {
                    case 4:
                        green.SetActive(true);
                        yellow.SetActive(false);
                        orange.SetActive(false);
                        red.SetActive(false);
                        blue.SetActive(false);
                        brickParticles = fourHitBrickParticles;
                        break;
                    case 3:
                        green.SetActive(false);
                        yellow.SetActive(true);
                        orange.SetActive(false);
                        red.SetActive(false);
                        blue.SetActive(false);
                        brickParticles = threeHitBrickParticles;
                        break;
                    case 2:
                        green.SetActive(false);
                        yellow.SetActive(false);
                        orange.SetActive(true);
                        red.SetActive(false);
                        blue.SetActive(false);
                        brickParticles = twoHitBrickParticles;
                        break;
                    case 1:
                        green.SetActive(false);
                        yellow.SetActive(false);
                        orange.SetActive(false);
                        red.SetActive(true);
                        blue.SetActive(false);
                        brickParticles = oneHitBrickParticles;
                        break;
                }
                break;
        }
        
        
        //Get the Renderer component from this object
        var Renderer = gameObject.GetComponent<Renderer>();

        ////Call SetColor using the shader property name "_Color" and setting the color to red
        //Renderer.material.SetColor("_Color", currentColor);



    }
}
