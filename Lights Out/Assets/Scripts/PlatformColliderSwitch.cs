using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;

public class PlatformColliderSwitch : MonoBehaviour
{
    public enum PlatformTypes
    {
        Dark,
        Light
    }

    public PlatformTypes platformType;
    BoxCollider2D boxCol;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        SwitchCollision();
    }

    private void OnEnable() => TimePhaseManager.OnPhaseChanged += SwitchCollision;
    
    private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SwitchCollision;


    private bool PlatformIsDark()
    {
        return platformType == PlatformTypes.Dark;
    }
    private void SwitchCollision()
    {
        if (TimePhaseManager.Instance.IsLight())
        {
            boxCol.enabled = !PlatformIsDark(); // if platform is dark, we set the collider to disabled if it's light out
        }
        else
        {
            boxCol.enabled = PlatformIsDark();
        }
    }

}
