using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*
*   Provides static references for AudioManager
*   All AudioManager calls are duplicated here
*
**/
public static class Audio
{
    static AudioManager am;

    public static void Setup(AudioManager AM) {
        am = AM;
    }

    public static void Prefs() {
        changeVolume(0, PlayerPrefs.GetFloat("Master", 0.8f));
        changeVolume(1, PlayerPrefs.GetFloat("SFX", 0.8f));
        changeVolume(2, PlayerPrefs.GetFloat("Music", 0.8f));
    }

    public static void play (string name) {
        am.play(name);
    }

    public static void changeVolume (string name, float v) {
        am.changeVolume(name, v);
    }

    public static void changeVolume (float v) {
        am.changeVolume(v);
    }

    public static void changeVolume (int type, float v) {
        am.changeVolume(type, v);
    }

    public static void updateMusic (string state) {
        am.updateMusic(state);
    }

    public static float getVolume(int i) {
        return am.volumes[i];
    }
}
