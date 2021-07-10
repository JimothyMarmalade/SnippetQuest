/*
 * Created by Logan Edmund, 5/11/21
 * Last Modified by Logan Edmund, 6/24/21
 * 
 * Controls the playback of BGM, SFX, and other sounds.
 * The AudioManager is a single, persistent object that will exist through the entire game. It will not be stored in individual levels.
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    //AllGameSounds stores the slugs for all sound clips used in-game - music, SFX, etc.
    [Header("Game-Wide AudioClip References")]
    public AudioClip[] GeneralSFX;

    [Header("Level AudioClip References")]
    public AudioClip[] LeadParkBGM;
    public AudioClip[] LeadParkSFX;

    [Header("Loaded Sounds")]
    //levelSounds stores all the clips that will be used in this level  - music, SFX, etc.
    public List<Sound> loadedSounds;

    public enum LoadedSoundCollection { None, Level_LeadPark, Debug_AllSounds }
    public LoadedSoundCollection currentSoundCollection;

    //the three BGM variables keep track of references for the current level's dynamic BGM tracks.
    public enum CurrentBGM {None, Exploration, Activity, Snippet}
    [Header("Active Dynamic BGM Tracks")]
    public bool BGMPlaying;
    public CurrentBGM focusedBGM;


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

        focusedBGM = CurrentBGM.None;
        ClearLoadedSounds();

        LoadPrioritySounds();

        BGMPlaying = false;
    }

    private void Start()
    {

    }

    //Loads all sounds. Based off the input sound collection, loads in sounds and creates AudioSources for them.
    public void LoadSoundCollection(LoadedSoundCollection c)
    {
        if (c == currentSoundCollection)
        {
            Debug.Log("LoadSoundCollection() returned early because the sonud collection did not change.");
            return;
        }

        ClearLoadedSounds();

        switch (c)
        {
            case LoadedSoundCollection.Debug_AllSounds:
                Debug.LogWarning("Debug_AllSounds has not been implemented yet!");
                break;
            case LoadedSoundCollection.Level_LeadPark:
                foreach (AudioClip ac in LeadParkSFX)
                {
                    loadedSounds.Add(CreateSound(ac, false, false, 1, 1));
                }
                foreach (AudioClip ac in LeadParkBGM)
                {
                    loadedSounds.Add(CreateSound(ac, false, true, 0.5f, 1));
                }
                currentSoundCollection = LoadedSoundCollection.Level_LeadPark;
                break;
        }
    }

    public void LoadPrioritySounds()
    {
        foreach (AudioClip ac in GeneralSFX)
        {
            loadedSounds.Add(CreateSound(ac, true, false, 1, 1));
        }
    }

    //Empties the loadedSounds list of non-priority SFX and destroys all current audiosources.
    public void ClearLoadedSounds()
    {
        ClearDynamicBGMSounds();

        foreach (Sound s in loadedSounds)
        {
            if (!s.priority)
            {
                Destroy(s.source);
                loadedSounds.Remove(s);
            }
        }
        currentSoundCollection = LoadedSoundCollection.None;
        BGMPlaying = false;
    }

    //Creates a new Sound object when fed an AudioClip and some modifiers.
    public Sound CreateSound(AudioClip clip, bool priority, bool loop, float volume, float pitch)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        Sound s = new Sound(source, clip, priority, loop, volume, pitch);

        return s;
    }

    public void PlaySound(string soundName)
    {
        Debug.Log("Playing sound " + soundName);
        Sound s = loadedSounds.Find(item => item.name == soundName);

        if (s == null)
        {
            Debug.LogError(soundName + " was not found in sounds in AudioManager!");
            return;
        }
        s.source.Play();
    }

    #region DynamicBGM Methods
    //Sets the references to the different BGM tracks.
    public void SetDynamicBGMSounds(string ExplorationBGM, string SnippetBGM, string ActivityBGM)
    {
        foreach (Sound s in loadedSounds)
        {
            levelExplorationBGM = loadedSounds.Find(item => item.name == ExplorationBGM);
            levelSnippetBGM = loadedSounds.Find(item => item.name == SnippetBGM);
            levelActivityBGM = loadedSounds.Find(item => item.name == ActivityBGM);
        }
    }

    //Clears the references to the current BGM tracks.
    public void ClearDynamicBGMSounds()
    {
        levelExplorationBGM = null;
        levelSnippetBGM = null;
        levelActivityBGM = null;
        focusedBGM = CurrentBGM.None;
        BGMPlaying = false;
    }


    //Method to initialize the dynamic BGM tracks for a level.
    public void StartBGM(CurrentBGM startingTrack)
    {
        //If the BGM hasn't started yet, run the regular version of this method.
        if (!BGMPlaying)
        {
            switch (startingTrack)
            {
                case CurrentBGM.Exploration:
                    focusedBGM = CurrentBGM.Exploration;
                    levelSnippetBGM.source.volume = 0;
                    levelActivityBGM.source.volume = 0;
                    break;
                case CurrentBGM.Activity:
                    focusedBGM = CurrentBGM.Activity;
                    levelExplorationBGM.source.volume = 0;
                    levelSnippetBGM.source.volume = 0;
                    break;
                case CurrentBGM.Snippet:
                    focusedBGM = CurrentBGM.Snippet;
                    levelExplorationBGM.source.volume = 0;
                    levelActivityBGM.source.volume = 0;
                    break;
            }

            levelExplorationBGM.source.Play();
            levelSnippetBGM.source.Play();
            levelActivityBGM.source.Play();
            BGMPlaying = true;
        }

    }

    public void BGMFocusExploration(float duration)
    {
        if (focusedBGM != CurrentBGM.Exploration)
        {
            focusedBGM = CurrentBGM.Exploration;
            FadeIn(levelExplorationBGM, duration);
            FadeOut(levelSnippetBGM, duration);
            FadeOut(levelActivityBGM, duration);
        }
    }

    public void BGMFocusSnippet(float duration)
    {
        if (focusedBGM != CurrentBGM.Snippet)
        {
            focusedBGM = CurrentBGM.Snippet;
            FadeOut(levelExplorationBGM, duration);
            FadeIn(levelSnippetBGM, duration);
            FadeOut(levelActivityBGM, duration);

        }
    }

    public void BGMFocusActivity(float duration)
    {
        if (focusedBGM != CurrentBGM.Activity)
        {
            focusedBGM = CurrentBGM.Activity;
            FadeOut(levelExplorationBGM, duration);
            FadeOut(levelSnippetBGM, duration);
            FadeIn(levelActivityBGM, duration);

        }
    }

    #endregion

    #region Music FadeIn/FadeOut Methods and Coroutines
    //Methods to trigger the fading in/out of BGM tracks
    public void FadeOut(string soundName, float duration)
    {
        Sound s = loadedSounds.Find(item => item.name == soundName);

        StartCoroutine(FadeOut(s, duration, Mathf.SmoothStep));
    }
    public void FadeOut(Sound s, float duration)
    {
        StartCoroutine(FadeOut(s, duration, Mathf.SmoothStep));
    }

    public void FadeIn(string soundName, float duration)
    {
        Sound s = loadedSounds.Find(item => item.name == soundName);

        StartCoroutine(FadeIn(s, duration, Mathf.SmoothStep));
    }
    public void FadeIn(Sound s, float duration)
    {
        StartCoroutine(FadeIn(s, duration, Mathf.SmoothStep));
    }

    //The IEnums where the actual work for fading tracks is done
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
    #endregion

    private bool CheckSoundExists(string soundName)
    {
        Sound s = loadedSounds.Find(item => item.name == soundName);

        if (s == null)
        {
            Debug.LogError(soundName + " was not found in sounds in AudioManager!");
            return false;
        }

        return true;
    }
}
