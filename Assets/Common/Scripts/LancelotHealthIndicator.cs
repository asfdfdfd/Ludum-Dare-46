using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LancelotHealthIndicator : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.DOFillAmount(GameState.Instance.LancelotHealth / (float) GameState.LancelotMaxHealth, 0.3f);
    }
}
