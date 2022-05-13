using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int value;
    float speed = 5f;
    bool normalFood = true;

    public LayerMask defaultLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (normalFood)
        {
            FoodManager.count++;
            StartingValues();
        }
        else
        {
            transform.Rotate(0f, 0f, Random.Range(0f, 365f));
        }      
    }

    // Starting values for normal foods
    void StartingValues()
    {
        float random_scale = Random.Range(0.2f, 0.8f);
        transform.localScale = new Vector3(random_scale, random_scale, 1f);

        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        value = Random.Range(2, 8);
    }

    // Starting values for foods derived from dead snakes
    public void StartingValues(float _scale, Sprite _sprite, Color _color, int _value)
    {
        transform.localScale = new Vector3(_scale, _scale, 1f);

        GetComponent<SpriteRenderer>().sprite = _sprite;
        GetComponent<SpriteRenderer>().color = _color;

        value = _value;
        normalFood = false;
    }

    public void OnCollected(Transform head, out int newValue)
    {
        gameObject.layer = defaultLayer;
        StartCoroutine(Collecting(head));
        Destroy(gameObject, 1f);
        newValue = value;
    }
     
    IEnumerator Collecting(Transform head)
    {
        while (true)
        {
            if (head != null)
            {
                transform.Translate((head.position - transform.position) * Time.deltaTime * speed, Space.World);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    void OnDestroy()
    {
        FoodManager.count--;
    }
}
