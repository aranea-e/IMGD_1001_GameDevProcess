using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAccess : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;

    void Awake() {
        int t = checkCurrent();
        for (int i = 0; i < 4; i++)
        {
            toggles[i].isOn = (i == t);
        }
    }

    int checkCurrent() {
        switch (FindObjectOfType<Controller>().mode)
        {
            case "point":
            return 1;

            case "joystick":
            return 2;

            case "controller":
            return 3;

            case "keyboard":
            default:
            return 0;
        }
    }

    public void changeControls(string c) {
        if (FindObjectOfType<Controller>().mode == c)
        {
            return;
        }
        Audio.play("select");
        FindObjectOfType<Controller>().mode = c;
    }
}
