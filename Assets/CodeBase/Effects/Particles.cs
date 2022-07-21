using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Effects
{
    public class Particles : MonoBehaviour
    {
        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private ParticleSystem _JumpUpVFX;
        [SerializeField] private ParticleSystem _JumpDownVFX;
        [SerializeField] private ParticleSystem _CollisionVFX;

        public void PlayJumpVFX()
        {
            if (_cubeMover.YVelocity != 0 && _cubeMover.IsJumpButtonPressed)
            {
                _JumpDownVFX.Play();
            }
            else if (_cubeMover.YVelocity == 0 && _cubeMover.IsJumpButtonPressed)
            {
                _JumpUpVFX.Play();
            }
        }

        public void PlayCollisionVFX()
        {
            _CollisionVFX.Play();
        }
    }
}