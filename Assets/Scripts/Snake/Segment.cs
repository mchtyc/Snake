using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public bool movable;
    public SnakeEvents snakeEvents;

    void OnEnable()
    {
        snakeEvents.PlayerDie += OnDie;
    }

    void OnDisable()
    {
        snakeEvents.PlayerDie -= OnDie;
    }

    // Segments become smaller, scattered around, and snake stops when game over
    protected virtual void OnDie()
    {
        movable = false;

        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        float edgePoint = 1f;
        transform.Translate(Random.Range(-edgePoint, edgePoint), Random.Range(-edgePoint, edgePoint), 0f, Space.Self);
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
    }
}