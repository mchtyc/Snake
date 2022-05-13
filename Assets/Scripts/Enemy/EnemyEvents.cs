using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    public delegate void EventHandler();


    public event EventHandler EventEnemyDie;


    public void OnEventEnemyDie()
    {
        if (EventEnemyDie != null)
        {
            EventEnemyDie();
        }
    }
}
