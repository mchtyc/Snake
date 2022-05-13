using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEvents : MonoBehaviour
{
    public delegate void SnakeEventHandler();


    public event SnakeEventHandler PlayerDie;


    public void OnEventPlayerDie()
    {
        if (PlayerDie != null )
        {
            PlayerDie();
        }
    }    
}
