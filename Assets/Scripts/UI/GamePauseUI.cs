using System;
using KitchenChaos.Manager.GameStates;
using KitchenChaos.UI.Options;
using KitchenChaos.UI.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private AudioSource _gameTimerAudioSource;

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonPress);
            _optionsButton.onClick.AddListener(OnOptionsButtonPress);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonPress);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonPress);
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGamePaused += GamePauseUI_OnGamePause;
            KitchenGameManager.Instance.OnGameUnPaused += GamePauseUI_OnGameUnPause;
            Hide();
        }

        private void OnOptionsButtonPress()
        {
            OptionsMenuUI.Instance.Show();
        }

        private void GamePauseUI_OnGamePause(object sender, EventArgs e)
        {
            Show();
        }
        
        private void GamePauseUI_OnGameUnPause(object sender, EventArgs e)
        {
            Hide();
            _gameTimerAudioSource.Play();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false) ;
        }
        
        private void OnMainMenuButtonPress()
        {
            Loader.Load(Loader.Scene.MainMenu);
        }
    }
}

