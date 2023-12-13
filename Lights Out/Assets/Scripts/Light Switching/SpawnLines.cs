using System;
using System.Collections;
using UnityEngine;

namespace Light_Switching
{
    public class SpawnLines : MonoBehaviour
    {
        
        private PlatformColliderSwitch.PlatformTypes platformType;
        private PlatformColliderSwitch platformParent;
        
        public GameObject dottedLine;
        [SerializeField] private float lineSpeed = 1;
        [SerializeField] private float timeDifference = .5f;
        [SerializeField] private float numOfLines;

        
        private void OnEnable() => TimePhaseManager.OnPhaseChanged += SpawnPreLines;
        private void OnDisable() => TimePhaseManager.OnPhaseChanged -= SpawnPreLines;

        private void Start()
        {
            SpawnPreLines();
            platformType = GetComponentInParent<PlatformColliderSwitch>().platformType;
        }

        private void SpawnPreLines()
        {
            if (CheckPlatformStatus())
            {
                StartCoroutine(Spawner());
                for (int i = 0; i < numOfLines; i++)
                {
                    float xOffset = -timeDifference * i;
                    Vector3 spawnPosition = transform.position + transform.right * xOffset;

                    Instantiate(dottedLine, spawnPosition, transform.rotation);
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
