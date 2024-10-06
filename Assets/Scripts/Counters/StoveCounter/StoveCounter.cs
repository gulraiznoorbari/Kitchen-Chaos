using System;
using KitchenChaos.Data;
using KitchenChaos.Feature.Player;
using KitchenChaos.UI.Interface;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class StoveCounter : BaseCounter, IProgressBar
    {
        [SerializeField] private FryingRecipeSO[] FryingRecipeObjectsList;
        [SerializeField] private BurningRecipeSO[] BurningRecipeObjectsList;

        private float _fryingTimer;
        private FryingRecipeSO _fryingRecipeSO;
        private float _burningTimer;
        private BurningRecipeSO _burningRecipeSO;

        public enum FryingState
        {
            Idle,
            Frying,
            Fried,
            Burned
        }

        private FryingState _fryingState;
        public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<FryingStateChangedEventArg> OnFryingStateChanged;

        public class FryingStateChangedEventArg : EventArgs
        {
            public FryingState fryingState;
        }

        private void Start()
        {
            _fryingState = FryingState.Idle;
        }

        private void Update()
        {
            switch (_fryingState)
            {
                case FryingState.Idle:
                    break;
                case FryingState.Frying:
                    FryingItem();
                    break;
                case FryingState.Fried:
                    FriedItem();
                    break;
                case FryingState.Burned:
                    break;
            }
        }

        private void FryingItem()
        {
            _fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
            {
                progressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
            });
            if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
            {
                GetKitchenObject().DestroyKitchenObject();
                KitchenObject.SpawnKitchenObject(_fryingRecipeSO.outputItem, this);
                _fryingState = FryingState.Fried;
                _burningTimer = 0f;
                _burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                OnFryingStateChanged?.Invoke(this, new FryingStateChangedEventArg
                {
                    fryingState = _fryingState
                });
            }
        }

        private void FriedItem()
        {
            _burningTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
            {
                progressNormalized = _burningTimer / _burningRecipeSO.burningTimer
            });
            if (_burningTimer > _burningRecipeSO.burningTimer)
            {
                GetKitchenObject().DestroyKitchenObject();
                KitchenObject.SpawnKitchenObject(_burningRecipeSO.burnedItem, this);
                _fryingState = FryingState.Burned;
                OnFryingStateChanged?.Invoke(this, new FryingStateChangedEventArg
                {
                    fryingState = _fryingState
                });
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }

        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    if (HasItemInFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        _fryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                        _fryingState = FryingState.Frying;
                        _fryingTimer = 0f;
                        OnFryingStateChanged?.Invoke(this, new FryingStateChangedEventArg
                        {
                            fryingState = _fryingState
                        });
                        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                        {
                            progressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
                        });
                    }
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    _fryingState = FryingState.Idle;
                    OnFryingStateChanged?.Invoke(this, new FryingStateChangedEventArg
                    {
                        fryingState = _fryingState
                    });
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
                else
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroyKitchenObject();
                            _fryingState = FryingState.Idle;
                            OnFryingStateChanged?.Invoke(this, new FryingStateChangedEventArg
                            {
                                fryingState = _fryingState
                            });
                            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                }
            }
        }

        private bool HasItemInFryingRecipe(KitchenObjectSO inputFryingObjectSO)
        {
            var inputFryingRecipeSO = GetFryingRecipeSO(inputFryingObjectSO);
            return inputFryingRecipeSO != null;
        }

        private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO outputKitchenObjectSO)
        {
            foreach (var fryingRecipeSO in FryingRecipeObjectsList)
            {
                if (fryingRecipeSO.inputItem == outputKitchenObjectSO)
                {
                    return fryingRecipeSO;
                }
            }

            return null;
        }

        private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO outputKitchenObjectSO)
        {
            foreach (var burningRecipeSO in BurningRecipeObjectsList)
            {
                if (burningRecipeSO.inputItem == outputKitchenObjectSO)
                {
                    return burningRecipeSO;
                }
            }

            return null;
        }
    }
}