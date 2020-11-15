using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInstructions : MonoBehaviour
{
    public GameObject instructions;
    public GameObject bricks;
    public int visibility;

    // Start is called before the first frame update
    void Start()
    {
        visibility = 1;
        bricks.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GM.instance.gameIsPaused == false)
        {
            if (visibility == 1)
            {
                visibility = 2;
                instructions.SetActive(false);
                bricks.SetActive(true);
            }
            else if (visibility == 2)
            {
                visibility = 0;
                bricks.SetActive(false);
            }
            else if (visibility == 0)
            {
                visibility = 1;
                instructions.SetActive(true);
            }

        }
    }
}
