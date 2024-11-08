using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] bool fire;
    [SerializeField] string mode = "keyboard";

    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode down = KeyCode.S;
    KeyCode right = KeyCode.D;
    KeyCode firekey = KeyCode.Space;
    KeyCode follow = KeyCode.Mouse0;
    bool alwaysFollow = false;

    void Update() {
        switch (mode)
        {
            case "keyboard":
            ReadKeyboard();
            break;

            case "point":
            ReadCursor();
            break;

            case "joystick":
            //todo
            break;

            case "controller":
            //todo
            break;

            default:
            direction = Vector2.zero;
            fire = false;
            break;
        }

        SetInputs();
    }

    void ReadKeyboard() {
        direction = Vector2.zero;
        if (Input.GetKey(up)) { direction.y += 1; }
        if (Input.GetKey(left)) { direction.x -= 1; }
        if (Input.GetKey(down)) { direction.y -= 1; }
        if (Input.GetKey(right)) { direction.x += 1; }
        fire = Input.GetKey(firekey);
    }

    void ReadCursor() {
        fire = Input.GetKey(firekey);
        Blaster b = FindObjectOfType<Blaster>();
        if (b == null || (!alwaysFollow && !Input.GetKey(follow))) {
            direction = Vector2.zero;
            return;
        }
        direction = Util.mousePosition() - (Vector2) b.transform.position;
    }

    void SetInputs() {
        PlayerInputs.direction = direction;
        PlayerInputs.fire = fire;
    }
}
