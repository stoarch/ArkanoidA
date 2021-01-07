using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Ball ball;
    [SerializeField]
    Paddle paddle;
    [SerializeField]
    List<Block> blocks;
    [SerializeField]
    GameObject leftBounds;
    [SerializeField]
    GameObject rightBounds;
    [SerializeField]
    GameObject upperBounds;
    [SerializeField]
    GameObject lowerBounds;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text hitsText;
    [SerializeField]
    Text winText;
    [SerializeField]
    Text loseText;
    [SerializeField]
    Text startRoundText;

    bool gameStarted = false;
    bool gameOver = false;
    bool isWin = false;

    int score = 0;
    int hits = 100;

    public GameObject LeftBounds => leftBounds;
    public GameObject RightBounds => rightBounds;
    public GameObject UpperBounds => upperBounds;
    public GameObject LowerBounds => lowerBounds; 

    private void Start()
    {
        ball.Launch();

        gameStarted = true;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    internal void RemoveBlock(Block ablock)
    {
        blocks.Remove(ablock);
        if(blocks.Count == 0)
        {
            winText.gameObject.SetActive(true);
            gameStarted = false;
            gameOver = true;
            isWin = true;
            Time.timeScale = 0;
        }
    }

    internal void HitPlayer()
    {
        hits -= 25;
        if (hits <= 0)
            hits = 0;

        hitsText.text = hits.ToString() + "%";

        if(hits == 0)
        {
            gameStarted = false;
            gameOver = true;
            isWin = false;

            ball.Stop();

            loseText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    internal void PrepareRound()
    {
        startRoundText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startRoundText.gameObject.SetActive(false);
            ball.Launch();
        }
    }
}
