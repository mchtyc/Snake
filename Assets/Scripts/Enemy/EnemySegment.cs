using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySegment : MonoBehaviour
{
    public GameObject foodPre;
    public Transform foodCollector;

    public bool movable;
    public EnemyEvents enemyEvents;
    public LayerMask foodLayer;

    void OnEnable()
    {
        enemyEvents.EventEnemyDie += OnEnemyDie;
        foodCollector = GameObject.FindGameObjectWithTag("Foods").transform;
    }

    void OnDisable()
    {
        enemyEvents.EventEnemyDie -= OnEnemyDie;
    }

    // Segments become smaller, scattered around, and snake stops when enemy dies
    protected void OnEnemyDie()
    {
        movable = false;
        Vector3 position_for_food = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                                                transform.position.y + Random.Range(-1f, 1f), 0f);
        Sprite sprite_for_food = GetComponent<SpriteRenderer>().sprite;
        Color color_for_food = GetComponent<SpriteRenderer>().color;

        Food newFood = Instantiate(foodPre, position_for_food, Quaternion.identity, foodCollector).GetComponent<Food>();
        newFood.StartingValues(0.5f, sprite_for_food, color_for_food, 10);

        Destroy(gameObject);
        
        //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        //float edgePoint = 1f;
        //transform.Translate(Random.Range(-edgePoint, edgePoint), Random.Range(-edgePoint, edgePoint), 0f, Space.Self);
        //transform.Rotate(0f, 0f, Random.Range(0f, 360f));

    }
}
