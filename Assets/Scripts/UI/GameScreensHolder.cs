using Effects;
using Game;
using Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(GameStatesReloader))]
    public class GameScreensHolder : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private PlaySoundEffects _soundEffects;

        private GameStatesReloader _gameStates;

        private void Awake()
        {
            _gameStates = GetComponent<GameStatesReloader>();
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            ShowStartScreen();
        }

        private void OnEnable()
        {
            _startScreen.PlayButtonClick += OnGameStartButtonClick;
            _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
            _pauseScreen.PauseButtonClick += OnPauseButtonClick;
            _gameScreen.GameStartButtonClick += OnGameStartButtonClick;
            _cube.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _startScreen.PlayButtonClick -= OnGameStartButtonClick;
            _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
            _pauseScreen.PauseButtonClick -= OnPauseButtonClick;
            _gameScreen.GameStartButtonClick -= OnGameStartButtonClick;
            _cube.GameOver -= OnGameOver;
        }

        public void OpenStartMenu()
        {
            ShowStartScreen();
        }

        private void ShowStartScreen()
        {
            _startScreen.Open();
            _gameOverScreen.Close();
            _pauseScreen.Close();
            _gameScreen.Close();
            _soundEffects.TurnDownPitch();
            Time.timeScale = 0;
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

        private void ResumeGame()
        {
            Time.timeScale = 1f;
            _pauseScreen.Close();
            _soundEffects.PlayFromFade();
        }

        private void StartGame()
        {
            Time.timeScale = 1f;
            _gameStates.Reset();
            _gameScreen.Open();
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