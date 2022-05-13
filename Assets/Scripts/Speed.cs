using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed
{
    public static float SlowDown(float speed)
    {
        speed = speed - (speed * 2f / 1000f);

        return speed;
    }
}
