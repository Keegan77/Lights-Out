using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.StartCoroutine(GameManager.Instance.DeathSequence());
        }
    }
}
