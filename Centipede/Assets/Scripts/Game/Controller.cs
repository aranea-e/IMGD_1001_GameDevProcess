using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] bool fire;
    public string mode = "keyboard";

    #region keyboard/mouse
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode down = KeyCode.S;
    KeyCode right = KeyCode.D;
    KeyCode firekey = KeyCode.Space;
    KeyCode follow = KeyCode.Mouse1;
    KeyCode firecursor = KeyCode.Mouse0;
    bool alwaysFollow = false;
    #endregion

    #region controller
    PlayerControls controls;
    [SerializeField] Vector2 controllerdirection;
    [SerializeField] bool controllershoot;
    [SerializeField] bool controllermenu;
    #endregion

    void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => controllerdirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => controllerdirection = Vector2.zero;

        controls.Gameplay.Shoot.performed += ctx => controllershoot = true;
        controls.Gameplay.Shoot.canceled += ctx => controllershoot = false;

        //controls.Gameplay.Menu.performed += ctx => controllermenu = true;
        //controls.Gameplay.Menu.canceled += ctx => controllermenu = false;
        //
        //controls.Gameplay.cStart.performed += ctx => controllerstart = true;
        //controls.Gameplay.cStart.canceled += ctx => controllerstart = false;
    }

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

    void ReadController() {
        fire = controllershoot;
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
