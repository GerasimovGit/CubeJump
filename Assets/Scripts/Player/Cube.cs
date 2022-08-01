using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(CubeMover))]
    public class Cube : MonoBehaviour
    {
        private readonly string _highscoreKey = "highscore";
        
        private CubeMover _mover;
        private int _score;

        public int Highscore { get; private set; }

        public event UnityAction GameOver;

        public event UnityAction<int> ScoreChanged;

        private void Awake()
        {
            _mover = GetComponent<CubeMover>();
        }

        private void Start()
        {
            GetHighscore();
        }

        public void Reset()
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

            if (Highscore < _score)
            {
                SetHighscore();
            }

            ScoreChanged?.Invoke(_score);
        }

        public void OnPickUpBoost()
        {
            _mover.ActivateBoost();
        }

        private void GetHighscore()
        {
            Highscore = PlayerPrefs.GetInt(_highscoreKey);
        }

        private void SetHighscore()
        {
            Highscore = _score;
            PlayerPrefs.SetInt(_highscoreKey, _score);
        }
    }
}