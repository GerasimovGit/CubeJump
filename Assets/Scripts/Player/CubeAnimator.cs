using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(CubeMover))]
    public class CubeAnimator : MonoBehaviour
    {
        private static readonly int JumpHash = Animator.StringToHash("isJumping");

        private Animator _animator;
        private CubeMover _cubeMover;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _cubeMover = GetComponent<CubeMover>();
        }

        private void Update()
        {
            SetAnimatorStates();
        }

        private void SetAnimatorStates()
        {
            _animator.SetBool(JumpHash, _cubeMover.YVelocity > 0);
        }
    }
}