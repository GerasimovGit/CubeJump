using UnityEngine;
using UnityEngine.Events;
using Zones;

namespace Player
{
    [RequireComponent(typeof(Cube), typeof(CubeMover))]
    public class CubeCollisionHandler : MonoBehaviour
    {
        private Cube _cube;
        private CubeMover _cubeMover;

        public UnityEvent OnObstacleHit;

        private void Start()
        {
            _cube = GetComponent<Cube>();
            _cubeMover = GetComponent<CubeMover>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Wall wall) || _cubeMover.YVelocity < 0)
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
                if (_cubeMover.YVelocity <= 0f)
                {
                    _cube.OnPickUpBoost();
                }
            }
        }
    }
}