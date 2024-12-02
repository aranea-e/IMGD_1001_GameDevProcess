using UnityEngine;
using System;
using UnityEngine.Audio;

    /*
    *   Code block modified from: 
    *   https://www.youtube.com/watch?v=6OT43pvUyfY
    */

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    string state = "";
    public float[] volumes = {.8f, .8f, .8f};

    void Awake() {
        Audio.Setup(this);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        } 
        Audio.Prefs();       
    }

    public void play (string name) {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
    }

    public void stop (string name) {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Stop();
    }

    public void stop () {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
    }

    public void changeVolume (string name, float v) {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.volume = v;
    }

    public void changeVolume (float v) {
        foreach (Sound s in sounds) {
            s.source.volume = v;
        }
    }

    public void changeVolume (int type, float v) {
        volumes[type] = v;
        if (type == 0) {
            foreach (Sound s in sounds) {
                s.source.volume = volumes[0] * volumes[s.type];
            }
        } else {
            foreach (Sound s in sounds) {
                if (s.type == type) {
                    s.source.volume = volumes[0] * volumes[s.type];
                }
            }
        }
    }

    public void updateMusic (string state) {
        if (this.state == state) {
            return;
        }
        if (state == "") {
            stop(this.state);
            this.state = state;
            return;
        }
        play(state);
        if (this.state != "")
        {
            stop(this.state);
        }
        this.state = state;
    }
}
