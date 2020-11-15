using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    private float timeLeft = 3.0f;
    public Text startText;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0f)
        {
            GM.instance.LevelCon.SetActive(false);
            timeLeft = 3.0f;
            GM.instance.LoadUpNextLevel();
        }
    }
}
