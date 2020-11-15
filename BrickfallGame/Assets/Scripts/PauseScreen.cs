using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public bool paused;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (paused == false)
            {
                paused = true;
                Cursor.visible = true;
                pauseScreen.SetActive(true);
                GM.instance.pausingGame();
            }
            else if (paused == true)
            {
                paused = false;
                Cursor.visible = false;
                pauseScreen.SetActive(false);
                GM.instance.unpausingGame();
            }
        }
    }
}
