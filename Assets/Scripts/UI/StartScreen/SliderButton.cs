using System;
using Match3;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class SliderButton : MonoBehaviour
{
    public float scale;
    public Slider slider;
    public TMP_Text text;
    public RectMask2D mask2D;
    public Image imageMask;
    private float sizeDeltaX;

    public GameObject pictureActive;
    public GameObject picturePassive;
    
    
    private void Start()
    {
        sizeDeltaX = imageMask.rectTransform.sizeDelta.x / 100;
    }

    public void SliderMovement()
    {
        scale = slider.value;
        text.text = Math.Round(scale) + "";
        mask2D.padding = new Vector4(slider.value * sizeDeltaX, 0, 0, 0);
        if (scale <= 0) scale = 0;
        picturePassive.SetActive(scale <= 0);
        pictureActive.SetActive(scale >= 1);
    }
}