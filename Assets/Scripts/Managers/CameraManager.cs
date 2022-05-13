using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public EventManager eventManager;
    Transform target;
    Vector3 distance;
    bool startToFollow;

    void OnEnable()
    {
        eventManager.EventPlayerDie += OnPlayerDie;
        eventManager.EventPlayerCreated += GetPlayer;
    }

    void OnDisable()
    {
        eventManager.EventPlayerDie -= OnPlayerDie;
        eventManager.EventPlayerCreated -= GetPlayer;
    }

    void LateUpdate()
    {
        if (startToFollow)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.position = target.position - distance;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30.2f, 30.2f),
                                         Mathf.Clamp(transform.position.y, -40.9f, 40.9f),
                                         transform.position.z);
    }

    void GetPlayer(Transform _player)
    {
        target = _player.GetChild(1);
        distance = target.position - transform.position;
        startToFollow = true;
    }

    void OnPlayerDie()
    {
        StartCoroutine(CameraPanOut());
    }

    // Camera pans out when game over
    IEnumerator CameraPanOut()
    {
        for (int i = 0; i < 200; i++)
        {
            Camera.main.orthographicSize += 0.005f;

            yield return new WaitForEndOfFrame();
        }
    }
}
