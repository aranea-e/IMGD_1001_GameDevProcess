using UnityEngine.Audio;
using UnityEngine;

    /*
    *   Code block modified from: 
    *   https://www.youtube.com/watch?v=6OT43pvUyfY
    */

[System.Serializable]
public class Sound {
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;

    public int type;

    [HideInInspector]
    public AudioSource source;
}
