using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM instance = null;

    public int lives = 3;
    public int amountOfBricksInLevel;
    public float reloadOrResetDelay = 1f;
    public int score;
    public int brickpoints;
    public int updateBrickPoints;
    public int ballCount;
    public int nextLevel;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public GameObject LevelCon;
    public GameObject gameOver;
    public GameObject youWon;
    //public GameObject bricksPrefab;
    public GameObject paddle;
    public GameObject ball;
    public float paddleSpeed;
    public float originalPaddleSpeed;
    public float ballSpeed;
    public float originalBallSpeed;
    public float timeLeft;
    public Transform cloneLevel;
    public GameObject clonePaddle;
    public GameObject cloneBall;
    public GameObject deathParticles;
    public GameObject MainCamera;
    public GameObject pointsParticles;
    public GameObject pointsParticlesSpot;
    public GameObject goodPowerupParticles;
    public GameObject badPowerupParticles;
    public GameObject lifeParticles;
    public GameObject lifeParticlesSpot;
    public GameObject BombIsGo;
    public bool youWin;
    public bool camfliping;
    public bool scoring;

    public AudioSource gameplayMusic; // music track
    // sounds
    public AudioSource oneHitBrick;
    public AudioSource twoHitBrick;
    public AudioSource threeHitBrick;
    public AudioSource fourHitBrick;
    public AudioSource indestructibleHitBrick;
    public AudioSource loseLife;
    public AudioSource gameOverSound;
    //public int bounce; // make random
    //public AudioSource bounceSound1;
    public AudioSource bounceSound;
    public AudioSource positivePowerUp;
    public AudioSource negativePowerUp;
    public AudioSource levelWin;
    public AudioSource Bomb;
    public AudioSource gainLife;
    public AudioSource depowerUp;

    // levels
    public Transform[] levels;
    public int currentLevelIndex;

    public bool gameIsPaused; // pausing game

    // Start is called before the first frame update
    void Start()
    {
        camfliping = false;
        scoring = false;
        youWin = false;
        Cursor.visible = false;
        gameIsPaused = false;
        originalBallSpeed = ballSpeed;
        originalPaddleSpeed = paddleSpeed;
        updateBrickPoints = brickpoints;

        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }


        Setup();
    }

    public void Setup()
    {
        gameplayMusic.Play();
        cloneLevel = Instantiate(levels[currentLevelIndex], transform.position, Quaternion.identity) as Transform;
        Debug.Log("Loaded new level.");
        amountOfBricksInLevel = GameObject.FindGameObjectsWithTag("Brick").Length;

        Invoke("SetupPaddleAndBall", 0.0f);
    }

    void SetupPaddleAndBall()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        cloneBall = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
        //Instantiate(bricksPrefab, transform.position, Quaternion.identity);

        clonePaddle.transform.position = new Vector3(0, -3.6f, 0); // sets paddle into the middle of screen
    }

    void CheckGameOver()
    {
        // level win
        if (amountOfBricksInLevel < 1)
        {
            if(youWin == false)
            {
                youWin = true;
                gameplayMusic.Stop();
                levelWin.Play();
                youWon.SetActive(true);
                Destroy(clonePaddle);
                if (currentLevelIndex < levels.Length)
                {
                    LevelCon.SetActive(true);
                }
            }
        }

        // game over
        if (lives < 1)
        {
            gameplayMusic.Stop();
            gameOverSound.Play();
            gameOver.SetActive(true);
            int highScore = PlayerPrefs.GetInt("HIGHSCORE");
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HIGHSCORE", score);

                highScoreText.text = "New High Score! " + score;
            }
            else
            {
                highScoreText.text = "High Score: " + highScore + "\n" + "Your Score: " + score + "\n" + "Can you beat this high score?";
            }
            Invoke("BackToMenu", 2f);

        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("BrickfallStartMenus");
    }

    public void LoadUpNextLevel()
    {
        youWin = false;
        Destroy(clonePaddle);
        Destroy(cloneBall);
        Debug.Log("Destroyed paddle and ball.");
        if (cloneLevel != null) Destroy(cloneLevel.gameObject); // destroys current level
        
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Length) // will end stage and bring player back to select screen
        {
            PlayerPrefs.SetInt("levelReached", nextLevel);
            SceneManager.LoadScene("BrickfallStartMenus");
        }
        else // allows next level to play
        {
            Debug.Log("Loading new level.");
            StartNextLevel();
        }
    }

    public void StartNextLevel()
    {
        Time.timeScale = 1f;
        youWon.SetActive(false);

        Setup();
    }

    void Reset()
    {
        Time.timeScale = 1f;
        score = 0;
        scoreText.text = "" + score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // sound functions
    public void fourHitBrickSound()
    {
        fourHitBrick.Play();
    }
    public void threeHitBrickSound()
    {
        threeHitBrick.Play();
    }
    public void twoHitBrickSound()
    {
        twoHitBrick.Play();
    }
    public void oneHitBrickSound()
    {
        oneHitBrick.Play();
    }
    public void indestructibleBrickSound()
    {
        indestructibleHitBrick.Play();
    }
    public void Bounce()
    {
        bounceSound.Play();
    }

    public void Scoring(int points)
    {
        if (scoring == false){
            scoring = true;
            score += points;

            scoreText.text = "" + score;
        }
    }

    public void GetLife()
    {
        Instantiate(lifeParticles, lifeParticlesSpot.transform.position, Quaternion.identity);
        lives++;
        livesText.text = "Lives: " + lives;
        gainLife.Play();
    }

    public void PaddleSizeUp()
    {
        Instantiate(goodPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        positivePowerUp.Play();
        clonePaddle.transform.localScale = new Vector3(2, 1f, 1f);
        Invoke("PaddleNormal", 5f);
    }

    public void PaddleSizeDown()
    {
        Instantiate(badPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        negativePowerUp.Play();
        clonePaddle.transform.localScale = new Vector3(0.5f, 1f, 1f);
        Invoke("PaddleNormal", 5f);
    }

    public void SlowBall()
    {
        Instantiate(goodPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        positivePowerUp.Play();
        ballSpeed = (originalBallSpeed / 2f);
        Invoke("PaddleNormal", 3f);
    }

    public void FastBall()
    {
        Instantiate(badPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        ballSpeed = originalBallSpeed + (originalBallSpeed / 2f);
        Invoke("PaddleNormal", 3f);
    }

    public void PaddleNormal()
    {
        if (clonePaddle != null) clonePaddle.transform.localScale = paddle.transform.localScale;
        ballSpeed = originalBallSpeed;
        brickpoints = updateBrickPoints;
        depowerUp.Play();
        //Debug.Log("Everything is normal");
    }

    public void SpawnBall()
    {
        Instantiate(goodPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        positivePowerUp.Play();
        cloneBall = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
        cloneBall = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
    }

    public void BlowUp()
    {
        if (youWin == false)
        {
            youWin = true;
            positivePowerUp.Play();
            Bomb.Play();
            Debug.Log("the bomb went off");
            Destroy(cloneLevel.gameObject);
            BombIsGo.SetActive(true);
            youWon.SetActive(true);
            LevelCon.SetActive(true);
            gameplayMusic.Stop();
            Destroy(cloneBall);
            Destroy(clonePaddle);
            Invoke("CleanUp", 1f);
        }
    }

    public void CleanUp()
    {
        BombIsGo.SetActive(false);
    }

    public void DoublePoints()
    {
        Instantiate(goodPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
        brickpoints = updateBrickPoints * 2;
        positivePowerUp.Play();
        Invoke("PaddleNormal", 10f);
    }

    public void CamFlip()
    {
        if (camfliping == false)
        {
            camfliping = true;
            Instantiate(badPowerupParticles, clonePaddle.transform.position, Quaternion.identity);
            negativePowerUp.Play();
            MainCamera.transform.Rotate(0f, 0f, 180f);
            Invoke("CamFlipBack", 5f);
        }
    }
    public void CamFlipBack()
    {
        if(camfliping == true)
        {
            camfliping = false;
            depowerUp.Play();
            MainCamera.transform.Rotate(0f, 0f, 180f);
        }
    }

    public void DestroyedPaddle()
    {
        Destroy(cloneBall);
    }

    public void LoseLife()
    {
        if (camfliping == true)
        {
            camfliping = false;
            depowerUp.Play();
            MainCamera.transform.Rotate(0f, 0f, 180f);
            lives--;
            livesText.text = "Lives: " + lives;
            loseLife.Play();
            Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
            Destroy(clonePaddle);
            PaddleNormal();
            CheckGameOver();
            if(lives >= 1)
            {
                Invoke("SetupPaddleAndBall", reloadOrResetDelay);
            }
        }
        else
        {
            lives--;
            livesText.text = "Lives: " + lives;
            loseLife.Play();
            if (clonePaddle != null)
            {
                Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
                Destroy(clonePaddle);
            }
            PaddleNormal();
            CheckGameOver();
            if (lives >= 1)
            {
                Invoke("SetupPaddleAndBall", reloadOrResetDelay);
            }
        }
    }

    public void LowerBrickAmount()
    {
        amountOfBricksInLevel--;
        CheckGameOver();
    }

    public void checkBrickGroup()
    {
        amountOfBricksInLevel = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void pausingGame()
    {
        gameIsPaused = true;
        gameplayMusic.Pause();
        paddleSpeed = 0.0f;
        Time.timeScale = 0.0f;
    }
    public void unpausingGame()
    {
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        gameplayMusic.UnPause();
        paddleSpeed = originalPaddleSpeed;
    }

    void Update()
    {
        checkBrickGroup();
        ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;
    }
}
