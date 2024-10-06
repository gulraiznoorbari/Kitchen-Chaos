using System;
using KitchenChaos.Feature.Input;
using KitchenChaos.Feature.Interaction;
using KitchenChaos.Manager.GameStates;
using UnityEngine;

namespace KitchenChaos.Feature.Player
{
    public class PlayerController: MonoBehaviour, IKitchenObjectParent
    {
        public static PlayerController Instance { get; private set; }

        public event EventHandler OnPickedSomething;
        public event EventHandler<OnSelectedCounterChangedEventSArgs> OnSelectedCounterChanged;
        public class OnSelectedCounterChangedEventSArgs: EventArgs
        {
            public BaseCounter selectedCounter;
        }
        
        [Header("Game Input")] 
        [SerializeField] private GameInput _gameInput;

        [Header("Player Movement")] 
        [SerializeField] private float _moveSpeed = 11.0f;

        [Header("Player Interactions")] 
        [SerializeField] private LayerMask _countersLayerMask;
        [SerializeField] private Transform _playerHoldPoint;

        private bool _canMove;
        private bool _isWalking;
        private float _rotationSpeed = 12.0f;
        private Vector3 _lastInteractDirection;
        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;

        private void Awake()
        {
            if (Instance != null) 
            {
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            _gameInput.OnInteractAction += GameInput_Interact;
            _gameInput.OnCuttingAction += GameInput_CuttingInput;
        }

        private void Update()
        {
            HandleMovementAndRotation();
            HandleInteractions();
        }

        private void GameInput_Interact(object sender, EventArgs args)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying()) return;
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact(this);
            }
        }
        
        private void GameInput_CuttingInput(object sender, EventArgs args)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying()) return;
            if (_selectedCounter != null)
            {
                _selectedCounter.CuttingInteractInput();
            }
        }

        private void HandleMovementAndRotation()
        {
            var inputVector = _gameInput.GetInputVectorNormalized();
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            var moveDistance = _moveSpeed * Time.deltaTime;
            var playerRadius = 0.6f;
            var playerHeight = 2f;
            
            // Check For Collisions in X and Z;
            _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
            if (!_canMove)
            {
                // Attempt only X movement:
                var moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                _canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
                if (_canMove)
                {
                    // Can move only on the X:
                    moveDirection = moveDirectionX;
                }
                else
                {
                    // Cannot move only in X.
                    // Can move in Z:
                    var moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    _canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                    if (_canMove)
                    {
                        // Can move only in Z:
                        moveDirection = moveDirectionZ;
                    }
                }
            }

            if (_canMove)
            {
                transform.position += moveDirection * moveDistance;
            }

            _isWalking = moveDirection != Vector3.zero;
            // Calculate rotation when move direction changes:
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, _rotationSpeed * Time.deltaTime);
        }

        private void HandleInteractions()
        {
            var inputVector = _gameInput.GetInputVectorNormalized();
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            var maxInteractionDistance = 2.0f;

            if (moveDirection != Vector3.zero)
            {
                _lastInteractDirection = moveDirection;
            }

            if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, maxInteractionDistance, _countersLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != _selectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

        private void SetSelectedCounter(BaseCounter baseCounter)
        {
            this._selectedCounter = baseCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventSArgs
            {
                selectedCounter = _selectedCounter
            });
        }

        public Transform GetCounterTopTransform()
        {
            return _playerHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this._kitchenObject = kitchenObject;
            if (kitchenObject != null)
            {
                OnPickedSomething?.Invoke(this, EventArgs.Empty); 
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}