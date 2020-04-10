using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AudioManager
{

    #region Singleton
    private static AudioManager instance;
    private AudioManager() { }
    public static AudioManager Instance { get { return instance ?? (instance = new AudioManager()); } }
    #endregion

    public Dictionary<string, AudioClip> soundDict;
    public void Initialize()
    {
        soundDict = Resources.LoadAll<AudioClip>("Sounds/").ToDictionary(x => x.name);
    }
}
