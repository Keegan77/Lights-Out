using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;

public class VisualColor : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnEnable() => TimePhaseManager.OnPhaseChanged += SwitchVisualColor;
    private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SwitchVisualColor;

    private void SwitchVisualColor()
    {
        cam.backgroundColor = TimePhaseManager.Instance.IsLight() ? Color.white : Color.black;
    }
}