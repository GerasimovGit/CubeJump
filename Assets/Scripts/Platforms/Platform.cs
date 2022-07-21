using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Transform _landPlace;

        public Vector2 Direction { get; private set; }

        private void Update()
        {
            SetDirection(Direction);
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void SetParent(Transform target)
        {
            target.SetParent(_landPlace, true);
        }

        public void RemoveFromParent(Transform target)
        {
            target.parent = null;
        }
    }
}