using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;

public class DottedLineMovement : MonoBehaviour
{
    private float speed = 1;
    private void Awake()
    {
        TimePhaseManager.OnPhaseChanged += LineDestruction;
    }

    private void OnDestroy()
    {
        TimePhaseManager.OnPhaseChanged -= LineDestruction;
    }

    private void Update()
    {
        transform.Translate(-transform.right * (speed * Time.deltaTime));
    }
    private void LineDestruction()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LineDeleter"))
        {
            Destroy(gameObject);
        }
    }
    
}