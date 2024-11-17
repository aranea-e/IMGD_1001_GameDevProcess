using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static MenuManager menu;

    public static Vector2 mousePosition() 
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
}
