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
}
