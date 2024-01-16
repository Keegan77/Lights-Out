using Light_Switching;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LightFellaBehaviour : MonoBehaviour
{
    public static LightFellaBehaviour Instance;
    [SerializeField] private Animator animator; 
    [SerializeField] AudioSource lightSwitchSound;
    private Transform nextLocation;
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
        SceneManager.sceneLoaded += UpdateStartLocation;
        TimePhaseManager.OnPhaseChanged += PlayLightSwitchSound;
        
        TransportLightFella += MoveLightFella;
    } 
    private void OnDisable()  
    {
        TimePhaseManager.StartFellaAnimation -= StartAnimation;
        SceneManager.sceneLoaded -= UpdateStartLocation;
        TimePhaseManager.OnPhaseChanged -= PlayLightSwitchSound;
        
        TransportLightFella -= MoveLightFella;
    }

    private void MoveLightFella(Transform location)
    {
        transform.position = location.position;
    }
    
    private void PlayLightSwitchSound()
    {
        lightSwitchSound.Play();
    }

    private void StartAnimation()
    {
        animator.SetTrigger("TimephaseAnimation");
    }
}
