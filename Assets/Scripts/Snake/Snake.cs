using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public static float speed = 4f, distanceBetweenSegments = 0.7f;
    public static int bodyCount = 1;
    public static int score = 0, tempScore = 0;
    public SnakeTrail snakeTrail;
    
    Text scoreBoard;

    void Start()
    {
        scoreBoard = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Text>();
    }
    public void CreateNewSegment()
    {
        snakeTrail.AddSegment();
    }

    public void CheckScore()
    {
        scoreBoard.text = score.ToString();
        if (score - tempScore >= 10)
        {
            bodyCount++;
            CreateNewSegment();
            tempScore += 10;
            CheckScore();
        }
    }
}
