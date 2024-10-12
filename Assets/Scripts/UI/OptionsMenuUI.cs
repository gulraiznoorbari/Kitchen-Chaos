using System;
using KitchenChaos.Feature.Input;
using KitchenChaos.Manager.Audio;
using KitchenChaos.Manager.GameStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.UI.Options
{
    public class OptionsMenuUI : MonoBehaviour
    {
        public static OptionsMenuUI Instance { get; private set; }
        
        [SerializeField] private Button _soundEffectsButton;
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _soundEffectsText;
        [SerializeField] private TextMeshProUGUI _musicText;
        [SerializeField] private GameObject _pressToRebindAlert;
        [Header("KeyBinding Texts")]
        [SerializeField] private TextMeshProUGUI _moveUpText;
        [SerializeField] private TextMeshProUGUI _moveDownText;
        [SerializeField] private TextMeshProUGUI _moveLeftText;
        [SerializeField] private TextMeshProUGUI _moveRightText;
        [SerializeField] private TextMeshProUGUI _pauseText;
        [SerializeField] private TextMeshProUGUI _interactText;
        [SerializeField] private TextMeshProUGUI _cuttingText;
        [Header("KeyBinding Buttons")]
        [SerializeField] private Button _moveUpButton;
        [SerializeField] private Button _moveDownButton;
        [SerializeField] private Button _moveLeftButton;
        [SerializeField] private Button _moveRightButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _interactButton;
        [SerializeField] private Button _cuttingButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        { 
            KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
            UpdateMusicValue();
            UpdateSoundEffectsValue();
            UpdateBindingTexts();
            HidePressToRebindAlert();
            Hide();
        }

        private void OnEnable()
        {
            _soundEffectsButton.onClick.AddListener(OnSoundEffectsButtonPress);
            _musicButton.onClick.AddListener(OnMusicButtonPress);
            _closeButton.onClick.AddListener(OnCloseButtonPress);
            AddKeyBindingsButtonListeners();
        }

        private void OnDisable()
        {
            _soundEffectsButton.onClick.RemoveListener(OnSoundEffectsButtonPress);
            _musicButton.onClick.RemoveListener(OnMusicButtonPress);
            _closeButton.onClick.RemoveListener(OnCloseButtonPress);
            RemoveKeyBindingsButtonListeners();
        }
        
        private void KitchenGameManager_OnGameUnPaused(object sender, EventArgs e)
        {
            Hide();
        }

        private void OnSoundEffectsButtonPress()
        {
            SoundManager.Instance.ChangeVolume();
            UpdateSoundEffectsValue();
        }

        private void OnMusicButtonPress()
        {
            MusicManager.Instance.ChangeVolume();
            UpdateMusicValue();
        }

        private void OnCloseButtonPress()
        {
            Hide();
        }

        private void UpdateSoundEffectsValue()
        {
            _soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        }
        
        private void UpdateMusicValue()
        {
            _musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        }

        private void UpdateBindingTexts()
        {
            _moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            _moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            _moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            _moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            _pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
            _interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            _cuttingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Cutting);
        }
        
        private void AddKeyBindingsButtonListeners()
        {
            _moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveUp));
            _moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveDown));
            _moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveLeft));
            _moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveRight));
            _interactButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact));
            _cuttingButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Cutting));
            _pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Pause));
        }

        private void RemoveKeyBindingsButtonListeners()
        {
            _moveUpButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.MoveUp));
            _moveDownButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.MoveDown));
            _moveLeftButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.MoveLeft));
            _moveRightButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.MoveRight));
            _interactButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.Interact));
            _cuttingButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.Cutting));
            _pauseButton.onClick.RemoveListener(() => RebindBinding(GameInput.Binding.Pause));
        }
        
        private void RebindBinding(GameInput.Binding binding)
        {
            ShowPressToRebindAlert();
            GameInput.Instance.RebindKey(binding, () =>
            {
                HidePressToRebindAlert();
                UpdateBindingTexts();
            });
        }

        private void ShowPressToRebindAlert()
        {
            _pressToRebindAlert.SetActive(true);
        }
        
        private void HidePressToRebindAlert()
        {
            _pressToRebindAlert.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
