using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaos.Feature.Input
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }
        private PlayerInputActions _playerControls;
        
        public event EventHandler OnInteractAction;
        public event EventHandler OnCuttingAction;
        public event EventHandler OnPauseAction;
        
        private void Awake()
        {
            Instance = this;
            _playerControls = new PlayerInputActions();
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
    }

}
