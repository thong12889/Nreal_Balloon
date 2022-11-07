using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;
using NRKernal.Record;

public class GameControl : MonoBehaviour
{
    public Transform balloonPrefab;
    public Text scoreDisplay;
    public Text timeCounter;
    public Button startBtn;
    public Button againBtn;
    public Image timeBG;
    public AudioSource GameMusic;
    public AudioSource finishSound;
    public GameObject firstPage;

    private bool gameBegin;

    private int _playerScore = 0;
    private int _multiplier = 1;
    private float _timeSinceLastSpawn = 0.0f;
    private float _timeToSpawn = 0.0f;
    private List<Transform> _balloons;

    private const int BALLOON_POOL = 30;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        gameBegin = false;
        timeBG.gameObject.SetActive(false);
        againBtn.gameObject.SetActive(false); 
    }

    public void Game()
    {

        GameMusic.Play();
        startTime = 30f;
        _playerScore = 0;
        startBtn.gameObject.SetActive(false);
        firstPage.SetActive(false);
        timeBG.gameObject.SetActive(true);
        gameBegin = true;
        if(_balloons == null)
        {
            _balloons = new List<Transform>();
            for (int i = 0; i < BALLOON_POOL; i++)
            {
                Transform balloon = Instantiate(balloonPrefab) as Transform;
                balloon.parent = this.transform;
                _balloons.Add(balloon);
            }
        }
        
        SpawnBalloon();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameBegin)
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _timeToSpawn)
            {
                SpawnBalloon();
            }

            startTime -= 1 * Time.deltaTime;
            timeCounter.text = startTime.ToString("0");
            
            if(startTime <= 0)
            {
                GameMusic.Stop();
                finishSound.Play();
                gameBegin = false;
                foreach (Transform b in _balloons)
                {
                    BalloonControl bs = b.GetComponent<BalloonControl>();
                    bs.Deactivate();
                }
                UpdateScoreDisplay();
                againBtn.gameObject.SetActive(true);
                scoreDisplay.gameObject.SetActive(true);
            }
        }
    }
    void SpawnBalloon()
    {
        _timeSinceLastSpawn = 0.0f;
        _timeToSpawn = UnityEngine.Random.Range(0.0f, 2.0f);
        foreach (Transform b in _balloons)
        {
            BalloonControl bs = b.GetComponent<BalloonControl>();
            if (bs && !bs.isActive)
            {
                bs.Activate();
                break;
            }
        }
    }
    public void AddPoints(int points = 1)
    {
        _playerScore += points;
        //UpdateScoreDisplay();
    }
    void UpdateScoreDisplay()
    {
        scoreDisplay.text = "Score: " + _playerScore.ToString();
    }
    void Restart()
    {
        /*if (m_VideoCapture.IsRecording)
        {
            StopVideoCapture();
        }*/
        againBtn.gameObject.SetActive(false);
        scoreDisplay.gameObject.SetActive(false);
        timeBG.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        firstPage.SetActive(true);
    }
    
}
