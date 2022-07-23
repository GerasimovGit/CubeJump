using Player;
using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(PlayParticlesEffects), typeof(PlaySoundEffects))]
    public class CubeEffectsPlayer : MonoBehaviour
    {
        [SerializeField] private CubeMover _cubeMover;

        private readonly string _boostId = "Boost";
        private readonly string _hitId = "Hit";
        private readonly string _jumpDownId = "JumpDown";
        private readonly string _jumpUpId = "JumpUp";

        private PlaySoundEffects _sound;
        private PlayParticlesEffects _particles;

        private void Awake()
        {
            _particles = GetComponent<PlayParticlesEffects>();
            _sound = GetComponent<PlaySoundEffects>();
        }

        public void PlayJumpVFX()
        {
            if (_cubeMover.IsJumpButtonPressed)
            {
                if (_cubeMover.YVelocity != 0)
                {
                    _particles.Play(_jumpDownId);
                    _sound.Play(_jumpDownId);
                }
                else if (_cubeMover.YVelocity == 0)
                {
                    _particles.Play(_jumpUpId);
                    _sound.Play(_jumpUpId);
                }
            }
        }

        public void PlayCollisionVFX()
        {
            if (_cubeMover.YVelocity > 0)
            {
                _sound.Play(_hitId);
            }

            _particles.Play(_hitId);
        }

        public void PlayBoostSfx()
        {
            _sound.Play(_boostId);
        }
    }
}