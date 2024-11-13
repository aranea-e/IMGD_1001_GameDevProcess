using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider[] s;

    void Awake () {
        for (int i = 0; i < s.Length; i++)
        {
            s[i].value = Audio.getVolume(i);
        }
    }

    public void changeVolume (int type) {
        Audio.changeVolume(type, s[type].value);
    }
}
