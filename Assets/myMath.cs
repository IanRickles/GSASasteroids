using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMath 
{
   static Vector2 bounds;
   public static Vector2 Wrap(Vector2 pos, Vector2 tbounds)
    {
        bounds = tbounds;
        return Wrap(pos);
    }

    public static Vector2 Wrap(Vector2 pos)
    {
        if (Mathf.Abs(pos.x) > bounds.x)
        {
            pos.x *= -1f;
            pos.x *= .95f;
        }
        if (Mathf.Abs(pos.y) > bounds.y)
        {
            pos.y *= -1f;
            pos.y *= .95f;
        }
        return pos;
    }
}
