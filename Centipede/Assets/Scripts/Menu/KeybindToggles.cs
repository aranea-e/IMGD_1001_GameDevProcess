using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindToggles : MonoBehaviour
{
    [SerializeField] int group;
    [SerializeField] Toggle[] toggles;
    Controller c;
    void Awake() {
        c = FindObjectOfType<Controller>();
        switch (group)
        {
            case 0:
            toggles[0].isOn = (c.firekey == KeyCode.Space);
            toggles[1].isOn = (c.firekey == KeyCode.Mouse0);
            break;

            case 1:
            toggles[0].isOn = ((c.follow == KeyCode.Mouse0) && !c.alwaysFollow);
            toggles[1].isOn = ((c.follow == KeyCode.Mouse1) && !c.alwaysFollow);
            toggles[2].isOn = (c.alwaysFollow);
            break;

            case 2:
            toggles[0].isOn = (c.firecursor == KeyCode.Space);
            toggles[1].isOn = (c.firecursor == KeyCode.Mouse0);
            break;

            case 3:
            toggles[0].isOn = ((c.drag == KeyCode.Mouse0) && !c.alwaysDrag);
            toggles[1].isOn = ((c.drag == KeyCode.Mouse1) && !c.alwaysDrag);
            toggles[2].isOn = (c.alwaysDrag);
            break;

            case 4:
            toggles[0].isOn = (c.firejoystick == KeyCode.Space);
            toggles[1].isOn = (c.firejoystick == KeyCode.Mouse0);
            break;
        }
    }

    void Update() {
        disableToggles();
    }

    void disableToggles() {
        switch (group)
        {
            case 0:
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = (c.mode == "keyboard");
            }
            break;

            case 1:
            case 2:
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = (c.mode == "point");
            }
            break;

            case 3:
            case 4:
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = (c.mode == "joystick");
            }
            break;
        }
    }

    public void updateControl(int i) {
        switch (group)
        {
            case 0:
            if ((i == 0 && toggles[0].isOn && !(c.firekey == KeyCode.Space)) 
            || (i == 1 && toggles[1].isOn && !(c.firekey == KeyCode.Mouse0)))
            {
                Audio.play("select");
            }
            if (i == 0 && toggles[0].isOn) { c.firekey = KeyCode.Space; 
                                             PlayerPrefs.SetInt("Keyboard Fire", 0); }
            if (i == 1 && toggles[1].isOn) { c.firekey = KeyCode.Mouse0; 
                                             PlayerPrefs.SetInt("Keyboard Fire", 1); }
            break;

            case 1:
            if ((i == 0 && toggles[0].isOn && !(c.follow == KeyCode.Mouse0 && c.alwaysFollow == false)) 
            || (i == 1 && toggles[1].isOn && !(c.follow == KeyCode.Mouse1 && c.alwaysFollow == false))
            || (i == 2 && toggles[2].isOn && !(c.alwaysFollow == true)))
            {
                Audio.play("select");
            }
            if (i == 0 && toggles[0].isOn) { c.follow = KeyCode.Mouse0; c.alwaysFollow = false; 
                                             PlayerPrefs.SetInt("Pointer Move", 0); };
            if (i == 1 && toggles[1].isOn) { c.follow = KeyCode.Mouse1; c.alwaysFollow = false; 
                                             PlayerPrefs.SetInt("Pointer Move", 1); };
            if (i == 2 && toggles[2].isOn) { c.alwaysFollow = true; 
                                             PlayerPrefs.SetInt("Pointer Move", 2); };
            break;

            case 2:
            if ((i == 0 && toggles[0].isOn && !(c.firecursor == KeyCode.Space)) 
            || (i == 1 && toggles[1].isOn && !(c.firecursor == KeyCode.Mouse0)))
            {
                Audio.play("select");
            }
            if (i == 0 && toggles[0].isOn) { c.firecursor = KeyCode.Space; 
                                             PlayerPrefs.SetInt("Pointer Fire", 0); }
            if (i == 1 && toggles[1].isOn) { c.firecursor = KeyCode.Mouse0; 
                                             PlayerPrefs.SetInt("Pointer Fire", 1); }
            break;

            case 3:
            if ((i == 0 && toggles[0].isOn && !(c.drag == KeyCode.Mouse0 && c.alwaysDrag == false)) 
            || (i == 1 && toggles[1].isOn && !(c.drag == KeyCode.Mouse1 && c.alwaysDrag == false))
            || (i == 2 && toggles[2].isOn && !(c.alwaysDrag == true)))
            {
                Audio.play("select");
            }
            if (i == 0 && toggles[0].isOn) { c.drag = KeyCode.Mouse0; c.alwaysDrag = false; 
                                             PlayerPrefs.SetInt("Joystick Move", 0); };
            if (i == 1 && toggles[1].isOn) { c.drag = KeyCode.Mouse1; c.alwaysDrag = false; 
                                             PlayerPrefs.SetInt("Joystick Move", 1); };
            if (i == 2 && toggles[2].isOn) { c.alwaysDrag = true; 
                                             PlayerPrefs.SetInt("Joystick Move", 2); };
            break;

            case 4:
            if ((i == 0 && toggles[0].isOn && !(c.firejoystick == KeyCode.Space)) 
            || (i == 1 && toggles[1].isOn && !(c.firejoystick == KeyCode.Mouse0)))
            {
                Audio.play("select");
            }
            if (i == 0 && toggles[0].isOn) { c.firejoystick = KeyCode.Space; 
                                             PlayerPrefs.SetInt("Joystick Fire", 0); }
            if (i == 1 && toggles[1].isOn) { c.firejoystick = KeyCode.Mouse0; 
                                             PlayerPrefs.SetInt("Joystick Fire", 1); }
            break;
        }
    }
}
