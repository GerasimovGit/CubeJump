using Effects;
using Level;
using Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(LevelData))]
    public class GameScreensHolder : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameHud _gameHud;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private SoundEffects _soundEffects;

        private LevelData _levelData;

        private void Awake()
        {
            _levelData = GetComponent<LevelData>();
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
            _levelData.Reset();
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