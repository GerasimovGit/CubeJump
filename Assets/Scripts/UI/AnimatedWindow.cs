using System;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnShow()
        {
            _animator.SetTrigger(Show);
        }

        public void OnHide()
        {
            _animator.SetTrigger(Hide);
        }
    }
}