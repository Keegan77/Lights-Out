using System;
using System.Collections;
using System.Collections.Generic;
using Light_Switching;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;
using Vector3 = System.Numerics.Vector3;

public class LightFellaBehaviour : MonoBehaviour
{
    public static LightFellaBehaviour Instance;
    [SerializeField] private Animator animator;
    private Transform nextLocation;
    
    public delegate void LightFellaRequest(Transform newLocation);
    public static event LightFellaRequest TransportLightFella;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    private void UpdateStartLocation(Scene _, LoadSceneMode __)
    {
        nextLocation = GameObject.FindWithTag("FellaStartLocation").transform;
        TransportLightFella?.Invoke(nextLocation);
    }
    private void OnEnable()
    {
        TimePhaseManager.StartFellaAnimation += StartAnimation;
        TransportLightFella += MoveLightFella;
        SceneManager.sceneLoaded += UpdateStartLocation;
    } 
    private void OnDisable()  
    {
        TimePhaseManager.StartFellaAnimation -= StartAnimation;
        TransportLightFella -= MoveLightFella;
        SceneManager.sceneLoaded -= UpdateStartLocation;
    }

    private void MoveLightFella(Transform location)
    {
        transform.position = location.position;
    }

    private void StartAnimation()
    {
        animator.SetTrigger("TimephaseAnimation");
    }
}
