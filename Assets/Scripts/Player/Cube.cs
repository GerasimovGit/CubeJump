using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(CubeMover))]
    public class Cube : MonoBehaviour
    {
        private CubeMover _mover;
        private int _score;

        public event UnityAction GameOver;

        public event UnityAction<int> ScoreChanged;

        private void Awake()
        {
            _mover = GetComponent<CubeMover>();
        }

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

        public void AddScore(int scoreToAdd)
        {
            _score += scoreToAdd;
            ScoreChanged?.Invoke(_score);
        }

        public void OnPickUpBoost()
        {
            _mover.ActivateBoost();
        }
    }
}