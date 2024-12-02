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
        switch (type)
        {
            case 0: PlayerPrefs.SetFloat("Master", s[0].value); break;
            case 1: PlayerPrefs.SetFloat("SFX", s[1].value); break;
            case 2: PlayerPrefs.SetFloat("Music", s[2].value); break;
        }
    }
}
