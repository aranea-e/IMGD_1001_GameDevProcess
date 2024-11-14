using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] bool fire;
    public string mode = "keyboard";

    #region keyboard
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode down = KeyCode.S;
    KeyCode right = KeyCode.D;
    KeyCode firekey = KeyCode.Space;
    #endregion

    #region pointer
    KeyCode follow = KeyCode.Mouse1;
    KeyCode firecursor = KeyCode.Mouse0;
    bool alwaysFollow = false;
    #endregion

    #region joystick
    [SerializeField] Vector2 pos;
    float sensitivity = 0.5f;
    KeyCode drag = KeyCode.Mouse1;
    KeyCode firejoystick = KeyCode.Space;
    bool alwaysDrag = false;
    #endregion

    #region controller
    PlayerControls controls;
    [SerializeField] Vector2 controllerdirection;
    [SerializeField] bool firecontroller;
    [SerializeField] bool controllermenu;
    #endregion

    void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => controllerdirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => controllerdirection = Vector2.zero;

        controls.Gameplay.Shoot.performed += ctx => firecontroller = true;
        controls.Gameplay.Shoot.canceled += ctx => firecontroller = false;

        //controls.Gameplay.Menu.performed += ctx => controllermenu = true;
        //controls.Gameplay.Menu.canceled += ctx => controllermenu = false;
        //
        //controls.Gameplay.cStart.performed += ctx => controllerstart = true;
        //controls.Gameplay.cStart.canceled += ctx => controllerstart = false;
    }

    void Update() {
        Joystick j = FindObjectOfType<Joystick>();
        if (j != null) { j.active = false; }

        switch (mode)
        {
            case "keyboard":
            ReadKeyboard();
            break;

            case "point":
            ReadCursor();
            break;

            case "joystick":
            if (j != null) { j.transform.position = pos; j.active = true; }
            ReadJoystick();
            break;

            case "controller":
            ReadController();
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
        fire = Input.GetKey(firecursor);
        Blaster b = FindObjectOfType<Blaster>();
        if (b == null || (!alwaysFollow && !Input.GetKey(follow))) {
            direction = Vector2.zero;
            return;
        }
        direction = Util.mousePosition() - (Vector2) b.transform.position;
    }

    void ReadJoystick() {
        fire = Input.GetKey(firejoystick);
        Joystick j = FindObjectOfType<Joystick>();
        if (j == null || (!alwaysDrag && !Input.GetKey(drag))) {
            direction = Vector2.zero;
            return;
        }
        direction = (Util.mousePosition() - (Vector2) j.transform.position) * sensitivity;
    }

    void ReadController() {
        fire = firecontroller;
        direction = controllerdirection;
    }

    void SetInputs() {
        PlayerInputs.direction = direction;
        PlayerInputs.fire = fire;
        PlayerInputs.menu = (controllermenu && (mode == "controller"));
    }

    #region controller inputs

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }


    #endregion
}
