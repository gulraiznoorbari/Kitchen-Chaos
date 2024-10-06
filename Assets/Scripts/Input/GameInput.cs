using System;
using UnityEngine;

namespace KitchenChaos.Feature.Input
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerControls;
        
        public event EventHandler OnInteractAction;
        public event EventHandler OnCuttingAction;
        
        private void Awake()
        {
            _playerControls = new PlayerInputActions();
            _playerControls.Player.Enable();

            _playerControls.Player.Interact.performed += Interact_Performed;
            _playerControls.Player.Cutting.performed += Cutting_Performed;
        }

        private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void Cutting_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnCuttingAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetInputVectorNormalized()
        {
            var inputVector = _playerControls.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }
    }

}
