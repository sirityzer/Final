using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float timeLeft = 60.0f;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text timeText;

    public AudioSource win;
    public AudioSource loose;
    public AudioSource regular;
    public bool particleWin;

    public GameObject background;
   

    private bool gameOver;
    private bool restart;
    private int score;



    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        particleWin = false;
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            Win();

        }
    }

    void Win()
    {
        winText.text = "You win! Game Created by Camille Wagner";
        restartText.text = "Press 'T' for Restart";
        gameOver = true;
        restart = true;
        regular.mute = true;
        win.mute = false;
        background.GetComponent<BGScroller>().scrollSpeed *= 5;
        particleWin = true;
        
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! Game Created by Camille Wagner";
        gameOver = true;
        regular.mute = true;
        loose.mute = false;
        background.GetComponent<BGScroller>().scrollSpeed *= 0;
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("SpaceShooter");

            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        timeLeft -= Time.deltaTime;
        timeText.text = "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            gameOverText.text = "Game Over! Game Created by Camille Wagner";
            gameOver = true;
            
        }
    }

    
 

}