using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EventManager eventManager;
    public GameObject playerPrefab, enemyPrefab;
    public Transform enemies;
    public int enemyCount = 30;
    Snake PlayerSnake;
    SnakeEvents snakeEvents;

    public Transform leftBottomBoundary, rightTopBoundary;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakingPlayer());
        StartCoroutine(MakingEnemies());
    }

    void OnDisable()
    {
        snakeEvents.PlayerDie -= OnPlayerDie;
    }

    IEnumerator MakingPlayer()
    {
        yield return new WaitForSeconds(1f);

        PlayerSnake = Instantiate(playerPrefab).GetComponent<Snake>();
        snakeEvents = PlayerSnake.gameObject.GetComponent<SnakeEvents>();
        snakeEvents.PlayerDie += OnPlayerDie;
        eventManager.OnEventPlayerCreated(PlayerSnake.transform);
    }

    IEnumerator MakingEnemies()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < enemyCount; i++) 
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            EnemySnake enemySnake = Instantiate(enemyPrefab,
                                    new Vector3(Random.Range(leftBottomBoundary.position.x + 2f, rightTopBoundary.position.x - 2f),
                                                Random.Range(leftBottomBoundary.position.y + 2f, rightTopBoundary.position.y - 2f), 0f),
                                    Quaternion.identity, enemies).GetComponent<EnemySnake>();

            enemySnake.ChangeSnakeColor(color);

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void NewSegmentButtonClicked()
    {
        if (PlayerSnake != null)
        {
            PlayerSnake.CreateNewSegment();
        }
    }

    // Raise global player die event
    void OnPlayerDie()
    {
        eventManager.OnEventPlayerDie();
    }
}
