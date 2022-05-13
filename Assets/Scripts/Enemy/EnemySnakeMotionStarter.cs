using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeMotionStarter : MonoBehaviour
{
    public List<EnemySegment> segments = new List<EnemySegment>();
    public EnemySnake enemySnake;

    Vector3 prePos;

    // Start is called before the first frame update
    void Start()
    {
        prePos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.deltaTime * enemySnake.speed, Space.Self);

        if (Vector3.Distance(transform.position, prePos) >= enemySnake.distanceBetweenSegments)
        {
            foreach (EnemyBodySegment bs in segments)
            {
                bs.movable = true;
            }
            Destroy(gameObject);
        }
    }
}