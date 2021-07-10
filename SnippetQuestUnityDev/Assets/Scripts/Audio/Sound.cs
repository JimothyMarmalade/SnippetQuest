/*
 * Created by Logan Edmund, 5/11/21
 * Last Modified by Logan Edmund, 5/11/21
 * 
 * Holds data retaining to how sounds in-game should be played back to the player.
 * 
 */

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    //if a sound is a priority sound, that means it is always loaded with the AudioManager.
    public bool priority;

    public AudioClip clip;

    public bool loop = false;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

    public Sound(AudioSource source, AudioClip clip, bool priority, bool loop, float volume, float pitch)
    {
        this.source = source;
        this.source.clip = clip;
        name = clip.name;
        this.priority = priority;
        this.source.loop = loop;
        this.source.volume = volume;
        this.source.pitch = pitch;
        this.source.playOnAwake = false;
    }


}
