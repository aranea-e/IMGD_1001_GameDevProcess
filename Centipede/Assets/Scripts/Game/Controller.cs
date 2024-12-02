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
    public KeyCode firekey = KeyCode.Space;
    #endregion

    #region pointer
    public KeyCode follow = KeyCode.Mouse1;
    public KeyCode firecursor = KeyCode.Mouse0;
    public bool alwaysFollow = false;
    #endregion

    #region joystick
    public Vector2 pos;
    public float sensitivity = 0.5f;
    public KeyCode drag = KeyCode.Mouse1;
    public KeyCode firejoystick = KeyCode.Space;
    public bool alwaysDrag = false;
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

        mode = PlayerPrefs.GetString("control mode", "keyboard");

        switch (PlayerPrefs.GetInt("Keyboard Fire", 0))
        {
            case 0: firekey = KeyCode.Space; break;
            case 1: firekey = KeyCode.Mouse0; break;
        }

        switch (PlayerPrefs.GetInt("Pointer Move", 0))
        {
            case 0: follow = KeyCode.Mouse0; alwaysFollow = false; break;
            case 1: follow = KeyCode.Mouse1; alwaysFollow = false; break;
            case 2: alwaysFollow = true; break;
        }

        switch (PlayerPrefs.GetInt("Pointer Fire", 0))
        {
            case 0: firecursor = KeyCode.Space; break;
            case 1: firecursor = KeyCode.Mouse0; break;
        }

        switch (PlayerPrefs.GetInt("Joystick Move", 0))
        {
            case 0: drag = KeyCode.Mouse0; alwaysDrag = false; break;
            case 1: drag = KeyCode.Mouse1; alwaysDrag = false; break;
            case 2: alwaysDrag = true; break;
        }

        switch (PlayerPrefs.GetInt("Joystick Fire", 0))
        {
            case 0: firejoystick = KeyCode.Space; break;
            case 1: firejoystick = KeyCode.Mouse0; break;
        }

        sensitivity = PlayerPrefs.GetFloat("sensitivity", 0.5f);
        pos.x = PlayerPrefs.GetFloat("xpos", 30.0f);
        pos.y = PlayerPrefs.GetFloat("ypos", -10.0f);
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
