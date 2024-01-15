using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFellaInversionController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float pulseAmount;
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("0.5 would be half a pulse cycle, 0.25 would be a quarter of a pulse cycle, etc.")]
    private float pulseOffset;
    Vector3 cachedScale;

    private void Start()
    {
        cachedScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        
        transform.localScale = new Vector3(cachedScale.x + CalculatePulse(),
                                           cachedScale.y + CalculatePulse(),
                                           cachedScale.z);
    }

    private float CalculatePulse()
    {
        return Mathf.Sin((Time.time + pulseOffset) * pulseSpeed) * pulseAmount;
    }
}
