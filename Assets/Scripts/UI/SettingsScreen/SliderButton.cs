using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class SliderButton : MonoBehaviour
{
    public float Sound;
    //public float Dem = 10;
    public Slider Slider;
    public TMP_Text TextSound;
    public RectMask2D mask2D;
    public Image imageMask;
    private float sizeDeltaX;

    private void Start()
    {
        sizeDeltaX = imageMask.rectTransform.sizeDelta.x / 100;
    }

    public void SliderMovement()
    {
        Sound -= Slider.value;
        TextSound.text = Math.Round(Sound)+ " %";
        mask2D.padding = new Vector4(Slider.value * sizeDeltaX, 0, 0, 0);
        if (Sound <= 0) Sound = 0;
    }
}