using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform foods;
    public GameManager gameManager;

    public static int count;

    int maxCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FoodFactory());
    }

    IEnumerator FoodFactory()
    {
        while (true) // TODO: Buraya oyunu herhangi bir þekilde durduran durum konabilir
        {
            if (count < maxCount)
            {
                Instantiate(foodPrefab, GetRandomPosition(), Quaternion.identity, foods);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(gameManager.leftBottomBoundary.position.x + 2f, 
                                        gameManager.rightTopBoundary.position.x - 2f), 
                           Random.Range(gameManager.leftBottomBoundary.position.y + 2f,
                                        gameManager.rightTopBoundary.position.y - 2f), 0f);
    }
}
