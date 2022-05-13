using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    public Snake snake;
    float searchRadius = 3f;
    public LayerMask foodLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPrameter();
    }

    void CheckPrameter()
    {
        Collider2D food = Physics2D.OverlapCircle(transform.position, searchRadius, foodLayer);

        if (food != null)
        {
            int foodValue;
            food.GetComponent<Food>().OnCollected(transform, out foodValue);
            Snake.score += foodValue;

            snake.CheckScore();
        }
    }
}
