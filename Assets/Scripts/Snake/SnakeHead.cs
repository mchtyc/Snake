using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : Segment
{
    public static bool moving = false;
    Transform touchPad, handle;

    Vector2 direction, startPos;
    float handleSizeMultiplier;

    void Start()
    {
        touchPad = GameObject.FindGameObjectWithTag("Touchpad").transform;
        handle = touchPad.GetChild(0);
        handleSizeMultiplier = (touchPad.GetComponent<RectTransform>().sizeDelta.x 
                              - handle.GetComponent<RectTransform>().sizeDelta.x) / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movable)
        {

#if UNITY_ANDROID && !UNITY_EDITOR

            TranslateSnakeInAndroid();
            RotateSnakeInAndroid();

#endif

#if UNITY_EDITOR

            TranslateSnakeInEditor();
            RotateSnakeInEditor();

#endif

        }
    }

    void TranslateSnakeInEditor()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            transform.Translate(Vector2.up * Time.deltaTime * Snake.speed, Space.Self);
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    void RotateSnakeInEditor()
    {
        Vector2 trans = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        float angle = Mathf.Atan2(trans.y, trans.x) * Mathf.Rad2Deg;

        if (angle != 0f)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    void TranslateSnakeInAndroid()
    {
        
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            

            if(t.phase == TouchPhase.Began)
            {
                startPos = t.position;
                touchPad.position = t.position;
            }
            else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                direction = (t.position - startPos).normalized;

                handle.position = new Vector2(touchPad.position.x + (direction.x * handleSizeMultiplier), 
                                                            touchPad.position.y + (direction.y * handleSizeMultiplier));
                moving = true;
            }

            transform.Translate(Vector2.up * Time.deltaTime * Snake.speed, Space.Self);
            
        }
        else
        {
            moving = false;
        }
    }

    void RotateSnakeInAndroid()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle != 0f)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }

    protected override void OnDie()
    {
        base.OnDie();

        moving = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border") || other.CompareTag("Enemy"))
        {
            snakeEvents.OnEventPlayerDie();
        }
    }
}
