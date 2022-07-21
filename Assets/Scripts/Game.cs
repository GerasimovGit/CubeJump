using ObjectPool;
using Player;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private PlatformGenerator _platformGenerator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private CubeTracker _cubeTracker;

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
        _gameOverScreen.Close();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _cube.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _cube.GameOver -= OnGameOver;
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        _platformGenerator.ResetPool();
        _platformGenerator.ResetGenerator();
        _cubeTracker.ResetCamera();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _cube.ResetPlayer();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }
}