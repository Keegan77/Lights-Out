using System;
using System.Collections;
using UnityEngine;


namespace Light_Switching
{
    public class SpawnLines : MonoBehaviour
    {
        
        [SerializeField] private PlatformColliderSwitch.PlatformTypes platformType;
        
        public GameObject dottedLine;
        [SerializeField] private float lineSpeed = 1;
        private float timeDifference;
        [SerializeField] private float numOfLines;

        
        private void OnEnable() => TimePhaseManager.OnPhaseChanged += SpawnPreLines;
        private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SpawnPreLines;

        private void Awake()
        {
            timeDifference = lineSpeed / 2;
        }

        private void Start()
        {
            SpawnPreLines();
        }

        private void SpawnPreLines()
        {
            if (CheckPlatformStatus())
            {
                StartCoroutine(Spawner());
                for (int i = 0; i < numOfLines; i++)
                {
                    Vector3 spawnPosition;
                    float xOffset = timeDifference * i;
                    if (gameObject.CompareTag("VerticalLines"))
                    {
                        spawnPosition = transform.position - transform.right * xOffset;
                    }
                    else
                    {
                        spawnPosition = transform.position + transform.right * xOffset;
                    }
                    
                    if (CheckPlatformStatus()) // must be checked again in case the timephase changed while the coroutine was running
                    {
                        Instantiate(dottedLine, spawnPosition, transform.rotation);
                    }
                }
            }
        }

        private bool CheckPlatformStatus()
        {
            return TimePhaseManager.Instance.IsLight() ^ platformType == PlatformColliderSwitch.PlatformTypes.Light; // ^ is a Xor gate. With this,
        }                                                               // we can check that the timephase is opposite
                                                                        // of our platform type
        private IEnumerator Spawner()
        {
            while (CheckPlatformStatus())
            {
                yield return new WaitForSeconds(timeDifference * lineSpeed);
                if (CheckPlatformStatus()) //final check to make sure 
                {
                    Instantiate(dottedLine, transform.position, transform.rotation);
                }
            }
        }
   
    }
}
