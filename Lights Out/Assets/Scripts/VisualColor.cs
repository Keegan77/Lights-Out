using Light_Switching;
using UnityEngine;

public class VisualColor : MonoBehaviour
{
    [SerializeField] GameObject inversionMask;
    public static VisualColor Instance;
    
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

    private void OnEnable() => TimePhaseManager.OnPhaseChanged += SwitchVisualColor;
    private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SwitchVisualColor;

    private void SwitchVisualColor()
    {
        inversionMask.SetActive(!TimePhaseManager.Instance.IsLight());
    }
}