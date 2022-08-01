using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class CubeTracker : MonoBehaviour
    {
        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _moveUpCameraSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private float _speedWhenBoost;

        private Coroutine _coroutine;
        private Vector3 _defaultPosition;
        private Vector3 _finalPosition;

        private void Start()
        {
            SetStartPosition();
        }

        private void Update()
        {
            MovePositionUp();
        }

        public void ResetCamera()
        {
            StopCurrentCoroutine();
            transform.position = _defaultPosition;
        }

        public void Follow()
        {
            StopCurrentCoroutine();
            CalculateFinalPosition();

            if (_cubeMover.IsBoostActivate)
            {
                _coroutine = StartCoroutine(ChangePosition(_finalPosition, _speedWhenBoost));
            }
            else
            {
                _coroutine = StartCoroutine(ChangePosition(_finalPosition, _speed));
            }
        }

        private void SetStartPosition()
        {
            transform.position =
                new Vector3(_cameraOffset.x, _cubeMover.transform.position.y + _cameraOffset.y, _cameraOffset.z);
            _defaultPosition = transform.position;
        }

        private void MovePositionUp()
        {
            Vector3 transformPosition = transform.position;
            transformPosition.y += _moveUpCameraSpeed * Time.deltaTime;
            transform.position = transformPosition;
        }

        private void StopCurrentCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void CalculateFinalPosition()
        {
            _finalPosition = new Vector3(0, _cubeMover.transform.position.y) + _cameraOffset;

            if (_cubeMover.IsBoostActivate)
            {
                _finalPosition.y += _cubeMover.NextPositionYAfterBoost;
            }
        }

        private IEnumerator ChangePosition(Vector3 finalPosition, float speed)
        {
            while (transform.position.y < finalPosition.y)
            {
                transform.position = Vector3.Lerp(transform.position, finalPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}