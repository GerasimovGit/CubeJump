using Player;
using TMPro;
using UnityEngine;

namespace ScoreData
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _highScore;
        
        private void OnEnable()
        {
            _cube.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _cube.ScoreChanged -= OnScoreChanged;
        }

        public void ShowHighScore()
        {
            _highScore.text = _cube.Highscore.ToString();
        }

        private void OnScoreChanged(int score)
        {
            _score.text = score.ToString();
        }
    }
}