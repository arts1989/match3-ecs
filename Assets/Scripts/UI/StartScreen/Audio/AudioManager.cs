using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

   public Audio[] Audios;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Audio aud in Audios)
        {
            aud.AudioSource = gameObject.AddComponent<AudioSource>();
            aud.AudioSource.clip = aud.clip;
            aud.AudioSource.volume = aud.volume;
            aud.AudioSource.loop = aud.loop;
        }
    }

    public void Play(string sound)
    {
        Audio aud = Array.Find(Audios, item => item.Name == sound);
        aud.AudioSource.Play();
    }

    public void Stop(string sound)
    {
        Audio aud = Array.Find(Audios, item => item.Name == sound);
        aud.AudioSource.Stop();
    }

    public static void SoundPause()
    {
        AudioListener.pause = !AudioListener.pause;
    }
    
    //FindObjectOfType<AudioManager>.Play("название аудио"); для проигрывания в нужном месте
}


