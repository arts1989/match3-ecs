using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer audioMixer;
    public Slider slider;

    private float _volumeValue;
    private const float _multiplier = 20f;

    private void Awake()
    {
       slider.onValueChanged.AddListener(HandleSliderValueChanged); 
    }

    private void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, (float)(Math.Log10(slider.value) * _multiplier));
        slider.value = (float)Math.Pow(10F, _volumeValue / _multiplier);
    }

    private void HandleSliderValueChanged(float value)
    {
        var _volumeValue = Math.Log10(value) * _multiplier;
        audioMixer.SetFloat(volumeParameter, (float)_volumeValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
    }
}
