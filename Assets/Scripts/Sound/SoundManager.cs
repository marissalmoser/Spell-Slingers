using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using FMOD;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private Dictionary<string, SoundEvent> sounds;

    private List<string> activeSoundNames = new List<string>();
    private List<EventInstance> activeSoundInstances = new List<EventInstance>();

    public Dictionary<string, int> instanceCounters = new Dictionary<string, int>();
    public Dictionary<string, Coroutine> attachedLoopCoroutines = new Dictionary<string, Coroutine>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// Runs setup necessary for sound to work.
    /// </summary>
    public Task Initialize()
    {
        sounds = SoundDatabase.instance.soundEvents;

        return Task.CompletedTask;
    }

    //USE TOLOWERCASE() TO MAKE SURE INPUT KEY IS PROPER.
    public void PlayUniversalOneShotSound(string key)
    {
        RuntimeManager.PlayOneShot(sounds[key.ToLower()].soundEvent);
    }

    public void PlayAttachedOneShotSound(string key, GameObject obj)
    {
        RuntimeManager.PlayOneShotAttached(sounds[key.ToLower()].soundEvent, obj);
    }

    /// <summary>
    /// Plays a looping sound not attached to any object. | HACKER
    /// </summary>
    /// <param name="key"></param>
    public void PlayLoopingSound(string key)
    {
        key = key.ToLower();
        
        if(instanceCounters.TryGetValue(key, out int value) == true)
        {
            instanceCounters[key]++;
        }
        else
        {
            instanceCounters.Add(key, 1);

            UnityEngine.Debug.Log(instanceCounters[key]);
        }

        EventInstance instance = RuntimeManager.CreateInstance(sounds[key].soundEvent);
        instance.start();

        activeSoundNames.Add(key + instanceCounters[key]);
        activeSoundInstances.Add(instance);
    }

    /// <summary>
    /// Stops a looping sound with given key. | BOTH
    /// </summary>
    /// <param name="key"></param>
    public void StopLoopingSound(string key, FMOD.Studio.STOP_MODE mode)
    {
        key = key.ToLower();

        int index = activeSoundNames.IndexOf(key);
        EventInstance instance = activeSoundInstances[index];
        activeSoundInstances.Remove(instance);
        instanceCounters[key.Substring(0, key.Length - 1)]--;

        instance.stop(mode);
    }

    /// <summary>
    /// Plays a looping sound that is attached to an object | HACKER.
    /// </summary>
    /// <param name="key"></param>
    public EventInstance PlayAttachedLoopingSound(string key, GameObject obj)
    {
        key = key.ToLower();

        //Adds to the counter for the amount of active instances of a particular sound.
        if (instanceCounters.TryGetValue(key, out int value) == true)
        {
            instanceCounters[key]++;
        }
        else
        {
            instanceCounters.Add(key, 1);
        }

        //Creates and starts playing instance
        EventInstance instance = RuntimeManager.CreateInstance(sounds[key].soundEvent);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(obj.transform));
        RuntimeManager.AttachInstanceToGameObject(instance, obj.transform);
        instance.start();

        activeSoundNames.Add(key + instanceCounters[key]);
        activeSoundInstances.Add(instance);

        return instance;
    }

    public void TestPlaySound(string key)
    {
        RuntimeManager.PlayOneShot(sounds[key.ToLower()].soundEvent);
    }
}
