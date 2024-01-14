using UnityEngine;

public class MoveLightFellaOnTrigger : MonoBehaviour
{
    [SerializeField] private Transform newLocation;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LightFellaBehaviour.Instance.TransportLightFella?.Invoke(newLocation);
            Destroy(gameObject);
        }
    }
}
