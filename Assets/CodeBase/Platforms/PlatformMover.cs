using UnityEngine;

namespace CodeBase.Platforms
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private Transform _path;
        [SerializeField] private float _speed;

        private int _currentPoint;
        private int _direction;
        private Platform _platform;
        private Transform[] _points;

        private void Awake()
        {
            _platform = GetComponent<Platform>();
            SetPoints();
        }

        private void FixedUpdate()
        {
            DoPatrol(out Vector3 direction);
            _platform.SetDirection(direction.normalized);
        }

        private void SetPoints()
        {
            _points = new Transform[_path.childCount];

            for (int i = 0; i < _path.childCount; i++)
            {
                _points[i] = _path.GetChild(i);
            }
        }

        private void DoPatrol(out Vector3 direction)
        {
            Transform target = _points[_currentPoint];
            var position = SetTargetPosition(target);
            transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
            direction = transform.position - position;

            if (transform.position.x == target.position.x)
            {
                _currentPoint++;

                if (_currentPoint >= _path.childCount)
                {
                    _currentPoint = 0;
                }
            }
        }

        private Vector3 SetTargetPosition(Transform target)
        {
            var position = target.position;
            position = new Vector3(position.x, transform.position.y, transform.position.z);
            target.position = position;
            return position;
        }
    }
}