using Effects;
using Level;
using Player;
using ScoreData;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(LevelProgress))]
    public class GameScreensHolder : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private Score _score;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameHud _gameHud;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private SoundEffects _soundEffects;

        private LevelProgress _levelProgress;

        private void Awake()
        {
            _levelProgress = GetComponent<LevelProgress>();
        }

        private void Start()
        {
            ShowStartScreen();
        }

        private void OnEnable()
        {
            _startScreen.PlayButtonClick += OnGameStartButtonClick;
            _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
            _pauseScreen.PauseButtonClick += OnPauseButtonClick;
            _gameHud.GameStartButtonClick += OnGameStartButtonClick;
            _cube.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _startScreen.PlayButtonClick -= OnGameStartButtonClick;
            _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
            _pauseScreen.PauseButtonClick -= OnPauseButtonClick;
            _gameHud.GameStartButtonClick -= OnGameStartButtonClick;
            _cube.GameOver -= OnGameOver;
        }

        public void ShowStartScreen()
        {
            _startScreen.Open();
            _score.ShowHighScore();
            _gameOverScreen.Close();
            _pauseScreen.Close();
            _gameHud.Close();
            _soundEffects.TurnDownPitch();
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            _pauseScreen.Close();
            _soundEffects.PlayFromFade();
        }

        private void OnGameStartButtonClick()
        {
            StartGame();
        }

        private void OnRestartButtonClick()
        {
            _gameOverScreen.Close();
            StartGame();
        }

        private void OnPauseButtonClick()
        {
            _pauseScreen.Open();
            _soundEffects.TurnDownPitch();
            Time.timeScale = 0f;
        }

        private void StartGame()
        {
            Time.timeScale = 1f;
            _gameHud.Open();
            _levelProgress.Reset();
            _startScreen.Close();
            _soundEffects.PlayFromFade();
        }

        private void OnGameOver()
        {
            _soundEffects.TurnDownPitch();
            _gameOverScreen.Open();
            Time.timeScale = 0;
        }
    }
}