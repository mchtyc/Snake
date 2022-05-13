using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventHandler();
    public delegate void EventPlayerHandler(Transform _player);


    public event EventHandler EventPlayerDie;
    public event EventPlayerHandler EventPlayerCreated;


    public void OnEventPlayerDie()
    {
        if (EventPlayerDie != null)
        {
            EventPlayerDie();
        }
    }

    public void OnEventPlayerCreated(Transform _player)
    {
        if(EventPlayerCreated != null)
        {
            EventPlayerCreated(_player);
        }
    }
}
