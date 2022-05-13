using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeTrail : EnemyBodySegment
{
    public GameObject bodySegmentPrefab;
    int UnAddedSegmentCount;
    
    public void AddSegment()
    {
        UnAddedSegmentCount++;
        CheckMovement();
    }

    void CheckMovement()
    {
        if (movable && UnAddedSegmentCount > 0)
        {
            UnAddedSegmentCount--;
            CreateNewBodySegment();
        }
        else
        {
            StartCoroutine(WaitingForAddingSegment());
        }
    }

    void CreateNewBodySegment()
    {
        EnemyBodySegment newBodySegment = Instantiate(bodySegmentPrefab, transform.position, transform.rotation, transform.parent)
                                          .GetComponent<EnemyBodySegment>();
        newBodySegment.NewSegmentData(prePositions,preRotations, preSegment);
        enemySnake.bodyCount++;
        newBodySegment.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (1000 - enemySnake.bodyCount);

        SlowEnemyDown();        
        preSegment = newBodySegment.transform;
        ClearLists();

        //StopMoving for a while to get behind of the new segment
        StartCoroutine(StoppingTrail2(preSegment));
    }


    // TODO: Þimdilik mesafeye göre deðilde süreye göre beklemeye geçildi. Sýkýntý çýkmazsa bura metod silinebilir
    IEnumerator StoppingTrail(Transform preS)
    {
        movable = false;

        while (!movable)
        {
            yield return new WaitForFixedUpdate();
            
            if (Vector3.Distance(transform.position, preS.position) >= enemySnake.distanceBetweenSegments)
            {
                movable = true;
                transform.SetAsLastSibling();
            }
        }
    }

    IEnumerator StoppingTrail2(Transform preS)
    {
        movable = false;

        float waitingTime = 0.7f / enemySnake.speed;
        yield return new WaitForSeconds(waitingTime);

        movable = true;
        transform.SetAsLastSibling();
    }

    IEnumerator WaitingForAddingSegment()
    {
        yield return new WaitForSeconds(0.5f);

        CheckMovement();
    }

    void SlowEnemyDown()
    {
        if (enemySnake.bodyCount < 50)
        {
            enemySnake.speed = Speed.SlowDown(enemySnake.speed);
        }
    }
}
