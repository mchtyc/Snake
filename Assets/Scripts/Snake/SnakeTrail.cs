using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTrail : BodySegment
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
        BodySegment newBodySegment = Instantiate(bodySegmentPrefab, transform.position, transform.rotation, transform.parent)
                                          .GetComponent<BodySegment>();
        newBodySegment.NewSegmentData(prePositions,preRotations, preSegment);
        Snake.bodyCount++;
        newBodySegment.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (1000 - Snake.bodyCount);

        SlowSnakeDown();
        preSegment = newBodySegment.transform;
        ClearLists();

        //StopMoving of trail for a while to get behind of the new segment
        StartCoroutine(StoppingTrail2(preSegment));
    }

    // Mesafeye göre bekleme þimdilik eski kod oldu. Alttaki süreye göre beklemede sýkýntý yaþanýrsa
        // ilerde buraya geri dönülebilir.
    IEnumerator StoppingTrail(Transform preS)
    {
        movable = false;

        while (!movable)
        {
            yield return new WaitForFixedUpdate();
            
            if (Vector3.Distance(transform.position, preS.position) >= Snake.distanceBetweenSegments)
            {
                movable = true;
                transform.SetAsLastSibling();
            }
        }
    }

    // TODO: Yeni segmentin geliþiyle kuyruðun süreye göre bekletilmesi.. Enemy Snake için de tekrarlanacak
    IEnumerator StoppingTrail2(Transform preS)
    {
        movable = false;

        float waitingTime = 0.7f / Snake.speed;
        yield return new WaitForSeconds(waitingTime);

        movable = true;
        transform.SetAsLastSibling();
    }

    IEnumerator WaitingForAddingSegment()
    {
        yield return new WaitForSeconds(0.5f);

        CheckMovement();
    }

    // Yýlan yeterince uzamamýþsa yýlaný yavaþlat
    void SlowSnakeDown()
    {
        if (Snake.bodyCount < 50)
        {
            Snake.speed = Speed.SlowDown(Snake.speed);
        }
    }
}
