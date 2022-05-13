using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    public float speed = 4f, distanceBetweenSegments = 0.7f;
    public int bodyCount = 1;
    public bool moving;
    public EnemySnakeTrail snakeTrail;
    public SpriteRenderer head, body, trail;
    public EnemyEvents enemyEvents;

    public int enemyScore = 0, tempScore = 0;

    void OnEnable()
    {
        enemyEvents.EventEnemyDie += OnEnemyDie;
    }

    void OnDisable()
    {
        enemyEvents.EventEnemyDie -= OnEnemyDie;
    }

    public void CreateNewSegment()
    {
        snakeTrail.AddSegment();
    }

    public void ChangeSnakeColor(Color c)
    {
        head.color = c;
        body.color = c;
        trail.color = c;
    }

    public void AddScore(int score)
    {
        enemyScore += score;

        if (enemyScore - tempScore >= 10)
        {
            CreateNewSegment();
            tempScore += 10;
        }
    }

    void OnEnemyDie()
    {
        Destroy(gameObject, 0.2f);
    }
}
