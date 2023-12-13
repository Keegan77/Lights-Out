using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;

public class GroundAnimMid : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite darkGround;
    [SerializeField] private Sprite lightGround;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        TimePhaseManager.OnPhaseChanged += ChangeSprite;
    }
    private void OnDisable()
    {
        TimePhaseManager.OnPhaseChanged -= ChangeSprite;
    }
    private void ChangeSprite()
    {
        spriteRenderer.sprite = TimePhaseManager.Instance.IsLight() ? lightGround : darkGround;
    }

}
