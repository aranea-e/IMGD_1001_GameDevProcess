using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickSliders : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    Controller c;
    void Awake() {
        c = FindObjectOfType<Controller>();
        sliders[0].value = c.sensitivity;
        sliders[1].value = c.pos.x;
        sliders[2].value = c.pos.y;
    }

    void Update() {
        for (int i = 0; i < 3; i++)
        {
            sliders[i].interactable = (c.mode == "joystick");
        }
    }

    public void updateControl(int i) {
        switch (i)
        {
            case 0:
            c.sensitivity = sliders[0].value;
            break;
            case 1:
            c.pos.x = sliders[1].value;
            break;
            case 2:
            c.pos.y = sliders[2].value;
            break;
        }
    }
}
