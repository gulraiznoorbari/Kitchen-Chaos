using System;
using KitchenChaos.UI.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KitchenChaos.UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonPress);
            _quitButton.onClick.AddListener(OnQuitButtonPress);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonPress);
            _quitButton.onClick.RemoveListener(OnQuitButtonPress);
        }

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        private void OnPlayButtonPress()
        {
            Loader.Load(Loader.Scene.Gameplay);
        }

        private void OnQuitButtonPress()
        {
            Application.Quit();
        }
    }
}
