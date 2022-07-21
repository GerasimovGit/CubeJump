using Player;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _cube.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _cube.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}