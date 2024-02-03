using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer audioMixer;

    private void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "MusicVolume" ? 0f : -80f);
        audioMixer.SetFloat(volumeParameter, volumeValue);
    }
}
