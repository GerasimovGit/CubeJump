using CodeBase.Zones;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Cube))]
    public class CubeCollisionHandler : MonoBehaviour
    {
        public UnityEvent OnObstacleHit;
        private Cube _cube;

        private void Start()
        {
            _cube = GetComponent<Cube>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Wall wall) || _cube.YVelocity < 0)
            {
                OnObstacleHit?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out GameOverZone gameOverZone))
            {
                _cube.Die();
            }
            else if (other.TryGetComponent(out PowerUp powerUp))
            {
                if (_cube.YVelocity <= 0f)
                {
                    _cube.OnPickUpBoost();
                }
            }
        }
    }
}