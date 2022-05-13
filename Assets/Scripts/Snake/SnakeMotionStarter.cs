using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMotionStarter : MonoBehaviour
{
    public List<Segment> segments = new List<Segment>();

    Vector3 prePos;

    // Start is called before the first frame update
    void Start()
    {
        prePos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f || Input.touchCount > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * Snake.speed, Space.Self);

            if (Vector3.Distance(transform.position, prePos) >= Snake.distanceBetweenSegments)
            {
                foreach (BodySegment bs in segments)
                {
                    bs.movable = true;
                }
                Destroy(gameObject);
            }
        }
    }
}