using UnityEngine;
using Zones;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out GameOverZone gameOverZone))
        {
            Destroy(gameObject);
        }
    }
}