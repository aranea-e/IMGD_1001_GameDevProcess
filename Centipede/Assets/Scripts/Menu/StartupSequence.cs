using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSequence : MonoBehaviour
{
    void LoadMenu() {
        Util.menu.LoadScene(1, false);
        Audio.updateMusic("menu");
    }
}
