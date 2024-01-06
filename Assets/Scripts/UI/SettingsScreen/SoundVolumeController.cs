using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundVolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Slider _slider;

    [SerializeField] private string _saveVolumeKey;
    [SerializeField] private string _sliderTag;

    [SerializeField] private float _volume;


    private void Awake()
    {
        if (PlayerPrefs.HasKey(_saveVolumeKey))
        {
            _volume = PlayerPrefs.GetFloat(_saveVolumeKey);
            _audio.volume = _volume;
            
            GameObject sliderObj = GameObject.FindWithTag(this._sliderTag);
            if (sliderObj != null)
            {
                _slider = sliderObj.GetComponent<Slider>();
                _volume = _slider.value;
            }
        }
        else
        {
            _volume = 0.5f;
            PlayerPrefs.SetFloat(_saveVolumeKey, _volume);
            _audio.volume = _volume;
        }
    }

    private void LateUpdate()
    {
        GameObject sliderObj = GameObject.FindWithTag(_sliderTag);
        if (sliderObj != null)
        {
            _slider = sliderObj.GetComponent<Slider>();
            _volume = _slider.value;


            if (_audio.volume != _volume)
            {
                PlayerPrefs.SetFloat(_saveVolumeKey, _volume);
            }
        }
        _audio.volume = _volume;
    }
}
