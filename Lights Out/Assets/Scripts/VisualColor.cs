using Light_Switching;
using UnityEngine;

public class VisualColor : MonoBehaviour
{
    public static VisualColor Instance;
    private float _goal = 1;
    [SerializeField] private float speed;
    [SerializeField] private GameObject inversionMask;
    private float _current, _target;
    
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
    }
    private void Start()
    {
        _target = 0;
    }

    void Update()
    {
        _current = Mathf.MoveTowards(_current, _target, speed * Time.deltaTime);

        inversionMask.transform.localScale = Vector3.Lerp(new Vector3(0, 1 ,1), 
                                            new Vector3(_goal, inversionMask.transform.localScale.y, 
                                                            inversionMask.transform.localScale.z), _current);
    }
    

    private void OnEnable() => TimePhaseManager.OnPhaseChanged += SwitchVisualColor;
    private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SwitchVisualColor;

    private void SwitchVisualColor()
    {
        _target = _target == 1 ? 0 : 1;
    }
}