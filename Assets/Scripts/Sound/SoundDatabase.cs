using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoundDatabase : MonoBehaviour
{
    public static SoundDatabase instance;

    [SerializeField] private FmodEvents db;

    public Dictionary<string, SoundEvent> soundEvents = new Dictionary<string, SoundEvent>();

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

    private async void Start()
    {
        //DELETE THIS AFTER TESTING.
        await Initialize();
        await SoundManager.instance.Initialize();
    }

    /// <summary>
    /// Initializes runtime dictionary containing the SoundEvents used to play sounds.
    /// </summary>
    public Task Initialize()
    {
        foreach(SoundEvent s in db.soundEventDb)
        {
            soundEvents.Add(s.key.ToLower(), s);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns a SoundEvent containing the key value and the EventReference used to play the sound.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public SoundEvent GetSoundEvent(string key)
    {
        return soundEvents[key];
    }
}
