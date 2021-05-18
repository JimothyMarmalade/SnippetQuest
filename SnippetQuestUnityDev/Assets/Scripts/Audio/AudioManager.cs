/*
 * Created by Logan Edmund, 5/11/21
 * Last Modified by Logan Edmund, 5/12/21
 * 
 * Controls the playback of BGM, SFX, and other sounds.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    public Sound[] sounds;

    
    public Sound levelExplorationBGM;
    public Sound levelActivityBGM;
    public Sound levelSnippetBGM;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        StartBGM("Forest_Exploration", "Forest_Snippet", "Forest_Activity");
    }

    public void Play(string soundName)
    {
        Debug.Log("Playing sound " + soundName);
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError(soundName + " was not found in sounds in AudioManager!");
            return;
        }
        s.source.Play();
    }

    public void StartBGM(string ExplorationBGM, string SnippetBGM, string ActivityBGM)
    {
        levelExplorationBGM = Array.Find(sounds, sound => sound.name == ExplorationBGM); ;
        levelSnippetBGM = Array.Find(sounds, sound => sound.name == SnippetBGM);
        levelActivityBGM = Array.Find(sounds, sound => sound.name == ActivityBGM);

        levelSnippetBGM.source.volume = 0;
        levelActivityBGM.source.volume = 0;

        levelExplorationBGM.source.Play();
        levelSnippetBGM.source.Play();
        levelActivityBGM.source.Play();

    }

    public void BGMFocusExploration(float duration)
    {
        FadeIn(levelExplorationBGM, duration);
        FadeOut(levelSnippetBGM, duration);
        FadeOut(levelActivityBGM, duration);
    }

    public void BGMFocusSnippet(float duration)
    {
        FadeOut(levelExplorationBGM, duration);
        FadeIn(levelSnippetBGM, duration);
        FadeOut(levelActivityBGM, duration);
    }

    public void BGMFocusActivity(float duration)
    {
        FadeOut(levelExplorationBGM, duration);
        FadeOut(levelSnippetBGM, duration);
        FadeIn(levelActivityBGM, duration);
    }

    public void FadeOut(string soundName, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        StartCoroutine(FadeOut(s, duration, Mathf.SmoothStep));
    }
    public void FadeOut(Sound s, float duration)
    {
        StartCoroutine(FadeOut(s, duration, Mathf.SmoothStep));
    }

    public void FadeIn(string soundName, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        StartCoroutine(FadeIn(s, duration, Mathf.SmoothStep));
    }
    public void FadeIn(Sound s, float duration)
    {
        StartCoroutine(FadeIn(s, duration, Mathf.SmoothStep));
    }

    public static IEnumerator FadeOut(Sound sound, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startVolume = sound.source.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            sound.source.volume = Interpolate(startVolume, 0, t);
            yield return null;
        }

        sound.source.volume = 0;
    }
    public static IEnumerator FadeIn(Sound sound, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        sound.source.volume = 0;

        float resultVolume = sound.volume;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            sound.source.volume = Interpolate(0, resultVolume, t);
            yield return null;
        }

        sound.source.volume = resultVolume;
    }

    private bool CheckSoundExists(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError(soundName + " was not found in sounds in AudioManager!");
            return false;
        }

        return true;
    }


}
