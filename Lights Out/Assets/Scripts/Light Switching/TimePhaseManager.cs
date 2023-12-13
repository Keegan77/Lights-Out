using System.Collections;
using UnityEngine;

namespace Light_Switching
{
    public class TimePhaseManager : MonoBehaviour
    {
        public static TimePhaseManager Instance;
        private bool isLight = true; // default to light
        private bool paused;
    
        private const float TimePhaseAnimLength = 1.667f; 
        [SerializeField] private float timer;
    
        /// <summary>
        /// Event for changing phase
        /// </summary>
        public delegate void PhaseChanged();

        public static event PhaseChanged StartFellaAnimation;
        public static event PhaseChanged OnPhaseChanged;

        private void Awake() // Setup singleton pattern
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
            StartCoroutine(TimerSwitchPhase());
        }

        private void SwitchPhase()
        {
            isLight = !isLight;
            OnPhaseChanged?.Invoke();
        }

        private IEnumerator TimerSwitchPhase()
        {
            while (!paused)
            {
                yield return new WaitForSeconds(timer - TimePhaseAnimLength);
                StartFellaAnimation?.Invoke();
                yield return new WaitForSeconds(TimePhaseAnimLength);
                if (paused) yield break; // Leave coroutine if paused
                SwitchPhase();
            }
        }

        /// <summary>
        /// Pauses and unpauses the current timephase. Use param true for pause, false for unpause
        /// </summary>
        /// <param name="pauseStatus"></param>
        public void PauseTimephase(bool pauseStatus)
        {
            paused = pauseStatus;
            if (!pauseStatus) // if we set pause to false, start the timer again
            {
                StartCoroutine(TimerSwitchPhase());
            }
        }
        /// <summary>
        /// Getter for checking if the timephase is paused or not
        /// </summary>
        /// <returns></returns>
        public bool GetPauseStatus()
        {
            return paused;
        }
        /// <returns>
        /// true if current time phase is light
        /// </returns>
        public bool IsLight()
        {
            return isLight;
        }
        /// <summary>
        /// Forces the time phase to light. Combine this with PauseTimePhase to stay on Light
        /// </summary>
        public void ForceLightTimePhase()
        {
            isLight = true;
            OnPhaseChanged?.Invoke();
        }
    }
}
