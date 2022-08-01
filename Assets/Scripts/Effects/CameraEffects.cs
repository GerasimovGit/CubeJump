using System.Collections;
using UnityEngine;

namespace Effects
{
    public class CameraEffects : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _amount;

        private Vector3 _originalPosition;
        private float _fakeDelta;
        private float _timeAtCurrentFrame;
        private float _timeAtLastFrame;

        private void Update()
        {
            _timeAtCurrentFrame = Time.realtimeSinceStartup;
            _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
            _timeAtLastFrame = _timeAtCurrentFrame;
        }

        public void Shake()
        {
            _originalPosition = gameObject.transform.localPosition;
            StartCoroutine(ShakeCamera(_duration, _amount));
        }

        private IEnumerator ShakeCamera(float duration, float amount)
        {
            while (duration > 0)
            {
                transform.localPosition = _originalPosition + Random.insideUnitSphere * amount;
                duration -= _fakeDelta;
                yield return null;
            }

            transform.localPosition = _originalPosition;
        }
    }
}