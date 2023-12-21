using Light_Switching;
using UnityEngine;
using UnityEngine.UI;

public class VisualColor : MonoBehaviour
{
    public static VisualColor Instance;
    private float _goal = 1;
    [SerializeField] private GameObject inversionMask;
    private float _current, _target;
    private Image inversionMaskImg;
    [SerializeField] private TransitionAnimationData transitionData;
    
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
        inversionMaskImg = inversionMask.GetComponent<Image>();
    }

    void Update()
    {
        _current = Mathf.MoveTowards(_current, _target, transitionData.speed * Time.deltaTime);

        inversionMask.transform.localScale = Vector3.Lerp(transitionData.startScale, transitionData.endScale, transitionData.MotionCurve.Evaluate(_current));
        inversionMaskImg.fillMethod = transitionData.fillMethod;
        inversionMaskImg.fillAmount = Mathf.Lerp(transitionData.startFill, transitionData.endFill, transitionData.MotionCurve.Evaluate(_current));
    }
    private void OnEnable() => TimePhaseManager.OnPhaseChanged += SwitchVisualColor;
    private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SwitchVisualColor;

    private void SwitchVisualColor()
    {
        _target = _target == 1 ? 0 : 1;
    }
}