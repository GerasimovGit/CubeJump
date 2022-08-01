using Effects;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(ParticlesEffects), typeof(SoundEffects))]
    public class CubeEffects : MonoBehaviour
    {
        [SerializeField] private CubeMover _cubeMover;
        
        private readonly string _boostId = "Boost";
        private readonly string _hitId = "Hit";
        private readonly string _jumpDownId = "JumpDown";
        private readonly string _jumpUpId = "JumpUp";

        private ParticlesEffects _particlesEffects;
        private SoundEffects _soundEffects;

        private void Awake()
        {
            _particlesEffects = GetComponent<ParticlesEffects>();
            _soundEffects = GetComponent<SoundEffects>();
        }

        public void PlayJumpVFX()
        {
            if (_cubeMover.YVelocity != 0)
            {
                _particlesEffects.Play(_jumpDownId);
                _soundEffects.Play(_jumpDownId);
            }
            else if (_cubeMover.YVelocity == 0)
            {
                _particlesEffects.Play(_jumpUpId);
                _soundEffects.Play(_jumpUpId);
            }
        }

        public void PlayCollisionVFX()
        {
            if (_cubeMover.YVelocity > 0)
            {
                _soundEffects.Play(_hitId);
            }

            _particlesEffects.Play(_hitId);
        }

        public void PlayBoostSfx()
        {
            _soundEffects.Play(_boostId);
        }
    }
}