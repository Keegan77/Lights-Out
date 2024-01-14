using Light_Switching;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LightFellaBehaviour : MonoBehaviour
{
    public static LightFellaBehaviour Instance;
    [SerializeField] private Animator animator;
    private Transform nextLocation;
    private Transform[] locationQueue;
    
    public delegate void LightFellaTransportRequest(Transform newLocation);
    public LightFellaTransportRequest TransportLightFella; // not declared as event because we want to invoke it from
                                                           // other scripts
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
        nextLocation = GameObject.FindWithTag("FellaStartLocation")?.transform;
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
