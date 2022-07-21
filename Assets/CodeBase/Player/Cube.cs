using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Player
{
    [RequireComponent(typeof(CubeMover))]
    public class Cube : MonoBehaviour
    {
        private CubeMover _mover;
        private int _score;

        public float YVelocity { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<CubeMover>();
        }

        private void Update()
        {
            YVelocity = _mover.YVelocity;
        }

        public event UnityAction GameOver;
        public event UnityAction<int> ScoreChanged;

        public void ResetPlayer()
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
            _mover.ResetCube();
        }

        public void Die()
        {
            GameOver?.Invoke();
        }

        public void AddScore()
        {
            _score++;
            ScoreChanged?.Invoke(_score);
        }

        public void OnPickUpBoost()
        {
            _mover.ActivateBoost();
        }
    }
}