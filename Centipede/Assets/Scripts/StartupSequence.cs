using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupSequence : MonoBehaviour
{
    void Start() {
        Util.menu.LoadScene(1, false);
    }
}
