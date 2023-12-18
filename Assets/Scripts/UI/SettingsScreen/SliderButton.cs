using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class SliderButton : MonoBehaviour
{
    public float sound;
    public Slider slider;
    public TMP_Text textSound;
    public RectMask2D mask2D;
    public Image imageMask;
    private float sizeDeltaX;

    private void Start()
    {
        sizeDeltaX = imageMask.rectTransform.sizeDelta.x / 100;
    }

    public void SliderMovement()
    {
        sound = slider.value;
        textSound.text = Math.Round(sound) + "";
        mask2D.padding = new Vector4(slider.value * sizeDeltaX, 0, 0, 0);
        if (sound <= 0) sound = 0;
    }
}