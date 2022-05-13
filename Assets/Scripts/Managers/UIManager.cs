using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public EventManager eventManager;
    public Text scoreText, scoreTextOnPanel;

    void OnEnable()
    {
        eventManager.EventPlayerDie += OnPlayerDie;
    }

    void OnDisable()
    {
        eventManager.EventPlayerDie -= OnPlayerDie;
    }

    public void OnClickStartAgain()
    {
        Snake.score = 0;
        Snake.tempScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnPlayerDie()
    {
        scoreText.text = "";
        StartCoroutine(OpenningGameOverPanel());
    }

    IEnumerator OpenningGameOverPanel()
    {
        scoreTextOnPanel.text = Snake.score.ToString();

        yield return new WaitForSeconds(1f);

        gameOverPanel.SetActive(true);
    }
}
