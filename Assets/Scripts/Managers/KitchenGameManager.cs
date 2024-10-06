using System;
using KitchenChaos.Manager.Audio;
using UnityEngine;

namespace KitchenChaos.Manager.GameStates
{
    public class KitchenGameManager : MonoBehaviour
    {
        public enum GameStates
        {
            WaitingForStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }
        
        public static KitchenGameManager Instance { get; private set; }
        public event EventHandler<GameStateChangedEventArg> OnGameStateChanged;
        public class GameStateChangedEventArg : EventArgs
        {
            public GameStates gameState;
        }

        private GameStates _gameStates;
        private float waitingToStartTimer = 0.05f;
        private float countdownToStartTimer = 3f;
        private float gamePlayingTimer;
        private float gamePlayingTimerMax = 20f;

        private void Awake()
        {
            Instance = this;
            _gameStates = GameStates.WaitingForStart;
        }

        private void Update()
        {
            switch (_gameStates)
            {
                case GameStates.WaitingForStart:
                    waitingToStartTimer -= Time.deltaTime;
                    if (waitingToStartTimer < 0f)
                    {
                        _gameStates = GameStates.CountdownToStart;
                        OnGameStateChanged?.Invoke(this, new GameStateChangedEventArg()
                        {
                            gameState = _gameStates
                        });
                    }
                    break;
                case GameStates.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0f)
                    {
                        _gameStates = GameStates.GamePlaying;
                        gamePlayingTimer = gamePlayingTimerMax;
                        OnGameStateChanged?.Invoke(this, new GameStateChangedEventArg()
                        {
                            gameState = _gameStates
                        });
                    }
                    break;
                case GameStates.GamePlaying:
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0f)
                    {
                        _gameStates = GameStates.GameOver;
                        OnGameStateChanged?.Invoke(this, new GameStateChangedEventArg()
                        {
                            gameState = _gameStates
                        });
                    }
                    
                    break;
                case GameStates.GameOver:
                    break;
            }
        }

        public bool IsGamePlaying()
        {
            return _gameStates == GameStates.GamePlaying;
        }

        public bool IsCountdownToStartActive()
        {
            return _gameStates == GameStates.CountdownToStart;
        }

        public bool IsGameOver()
        {
            return _gameStates == GameStates.GameOver;
        }

        public float GetCountdownTimerText()
        {
            return countdownToStartTimer;
        }

        public float GetGameplayTimerNormalized()
        {
            return 1 - (gamePlayingTimer / gamePlayingTimerMax);
        }
    }
}

