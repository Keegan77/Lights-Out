using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;

public class BigFellaAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable() => TimePhaseManager.StartFellaAnimation += StartAnimation;
    private void OnDisable() => TimePhaseManager.StartFellaAnimation -= StartAnimation;

    private void StartAnimation()
    {
        if (TimePhaseManager.Instance.IsLight())
        {
            animator.SetTrigger("TimePhaseLight");
        }
        else
        {
            animator.SetTrigger("TimePhaseDark");
        }
        
    }
}
