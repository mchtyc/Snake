using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeHead : EnemySegment
{
    public EnemySnake enemySnake;

    bool rotating, impactAlert;
    Coroutine normalRotation;

    public float rotSpeed, rotRaidus;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SettingMoving());
        StartCoroutine(OpenningHeadSensor());
        StartCoroutine(RotateSnake());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movable)
        {
            TranslateSnake();
        }
    }

    void TranslateSnake()
    {
        if (enemySnake.moving)
        {
            transform.Translate(Vector2.up * Time.deltaTime * enemySnake.speed, Space.Self);
        }
    }

    IEnumerator RotateSnake()
    {
        while (true)
        {
            if (!rotating && !impactAlert)
            {
                if (Random.Range(1, 6) > 3)
                    NormalRotation();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void NormalRotation()
    {
        normalRotation = StartCoroutine(ChangeDirection(Random.Range(0.04f, 0.1f), Random.Range(20f, 50f)));
    }

    void AlertRotation()
    {
        if (normalRotation != null)
        {
            StopCoroutine(normalRotation);
        }

        StartCoroutine(ChangeDirection(0.01f, 35f));
    }

    void RotateClockwise()
    {
        transform.Rotate(0f, 0f, -5f);
    }

    void RotateCounterClockwise()
    {
        transform.Rotate(0f, 0f, 5f);
    }

    IEnumerator ChangeDirection(float turn_speed, float turn_radius)
    {
        rotating = true;
        bool move_clockwise = (Random.Range(0, 2) == 0) ? true : false;

        for (int i = 0; i < turn_radius; i++)
        {
            if (move_clockwise)
                RotateClockwise();
            else
                RotateCounterClockwise();

            yield return new WaitForSeconds(turn_speed);
        }

        rotating = false;
        impactAlert = false;
    }
    
    void HeadSensor()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector2.up));
        if (hit.Length > 0)
        {
            foreach (RaycastHit2D h in hit)
            {
                if (h.distance < 4f)
                {
                    if (h.collider.CompareTag("Border") || h.collider.CompareTag("Player"))
                    {
                        impactAlert = true;
                        AlertRotation();
                    }
                    else if (h.collider.CompareTag("Enemy") && h.collider.transform.parent != transform.parent)
                    {
                        impactAlert = true;
                        AlertRotation();
                    }
                }
            }
        }
    }

    // Open sensor one time in every 0.4s
    IEnumerator OpenningHeadSensor()
    {
        while (true)
        {
            HeadSensor();

            yield return new WaitUntil(() => !impactAlert);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SettingMoving()
    {
        yield return new WaitForSeconds(5f);
        int rand;

        while (true)
        {
            rand = Random.Range(1, 11);
            if (rand == 1)
            {
                enemySnake.moving = false;
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
            else
            {
                enemySnake.moving = true;
                yield return new WaitForSeconds(Random.Range(3f, 7f));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border") || other.CompareTag("Player"))
        {
            enemyEvents.OnEventEnemyDie();
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.transform.parent != transform.parent)
            {
                enemyEvents.OnEventEnemyDie();
            }
        }
    }
}
