using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private float deathWaitTime;
        
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
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator WaitToDie()
    {
        deathSound.Play();
        yield return new WaitForSeconds(deathWaitTime);
        RestartGame();
    }
    
}
