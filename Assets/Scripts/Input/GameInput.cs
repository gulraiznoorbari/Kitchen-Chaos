using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos.Feature.Input
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }
        
        private PlayerInputActions _playerControls;
        private const string BINDINGS = "InputBindings";

        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            Cutting,
            Pause
        }

        public event EventHandler OnInteractAction;
        public event EventHandler OnCuttingAction;
        public event EventHandler OnPauseAction;

        private void Awake()
        {
            Instance = this;
            _playerControls = new PlayerInputActions();
            if (PlayerPrefs.HasKey(BINDINGS))
            {
                _playerControls.LoadBindingOverridesFromJson(PlayerPrefs.GetString(BINDINGS));
            }
            _playerControls.Player.Enable();

            _playerControls.Player.Interact.performed += Interact_Performed;
            _playerControls.Player.Cutting.performed += Cutting_Performed;
            _playerControls.Player.Pause.performed += Pause_Performed;
        }
 
        private void OnDestroy()
        {
            _playerControls.Player.Interact.performed -= Interact_Performed;
            _playerControls.Player.Cutting.performed -= Cutting_Performed;
            _playerControls.Player.Pause.performed -= Pause_Performed;
            _playerControls.Dispose();
        }

        private void Interact_Performed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void Cutting_Performed(InputAction.CallbackContext obj)
        {
            OnCuttingAction?.Invoke(this, EventArgs.Empty);
        }

        private void Pause_Performed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetInputVectorNormalized()
        {
            var inputVector = _playerControls.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                default:
                case Binding.MoveUp:
                    return _playerControls.Player.Move.bindings[1].ToDisplayString();
                case Binding.MoveDown:
                    return _playerControls.Player.Move.bindings[2].ToDisplayString();
                case Binding.MoveLeft:
                    return _playerControls.Player.Move.bindings[3].ToDisplayString();
                case Binding.MoveRight:
                    return _playerControls.Player.Move.bindings[4].ToDisplayString();
                case Binding.Interact:
                    return _playerControls.Player.Interact.bindings[0].ToDisplayString();
                case Binding.Cutting:
                    return _playerControls.Player.Cutting.bindings[0].ToDisplayString();
                case Binding.Pause:
                    return _playerControls.Player.Pause.bindings[0].ToDisplayString();
            }
        }

        public void RebindKey(Binding binding, Action OnRebindComplete)
        {
            _playerControls.Player.Disable();
            InputAction inputAction; 
            int bindingIndex;
            switch (binding)
            {
                default:
                case Binding.MoveUp:
                    inputAction = _playerControls.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = _playerControls.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = _playerControls.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = _playerControls.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _playerControls.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.Cutting:
                    inputAction = _playerControls.Player.Cutting;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = _playerControls.Player.Pause;
                    bindingIndex = 0;
                    break;
            }
           inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback => 
                { 
                    _playerControls.Player.Enable();
                    OnRebindComplete();
                    PlayerPrefs.SetString(BINDINGS, _playerControls.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                }).Start();
        }
    }
}