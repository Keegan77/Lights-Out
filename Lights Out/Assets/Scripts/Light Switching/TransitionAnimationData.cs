using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "Transition", menuName = "Timephase Transition Animation")]
public class TransitionAnimationData : ScriptableObject
{
    public float speed;
    [Tooltip("If not using scale, set all to 1")]
    public Vector3 startScale;
    public Vector3 endScale;
    [Tooltip("If not using fill, set both to 1")]
    public float startFill;
    public float endFill;

    public Image.FillMethod fillMethod;
    
    public AnimationCurve MotionCurve;
}
