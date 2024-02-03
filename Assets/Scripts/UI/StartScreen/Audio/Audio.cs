using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio

{
    public string Name;
    
    public AudioClip clip;
    
    [Range(0f, 100f)]
    public float volume = 50f;

    public bool loop = false;

    [HideInInspector] 
    public AudioSource AudioSource;
}
