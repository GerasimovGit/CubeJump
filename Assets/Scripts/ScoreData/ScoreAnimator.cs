using Player;
using UnityEngine;

namespace ScoreData
{
    [RequireComponent(typeof(Animator))]
    public class ScoreAnimator : MonoBehaviour
    {
        [SerializeField] private Cube _cube;

        private readonly int _scoreChanged = Animator.StringToHash("ScoreChanged");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _cube.ScoreChanged += OnCubeScoreChanged;
        }

        private void OnDisable()
        {
            _cube.ScoreChanged -= OnCubeScoreChanged;
        }

        private void OnCubeScoreChanged(int score)
        {
            if (score == 0) return;
            _animator.SetTrigger(_scoreChanged);
        }
    }
}