using System;
using System.Collections.Generic;
using KitchenChaos.Data;
using KitchenChaos.Feature.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KitchenChaos.Feature.Delivery
{
    public class DeliveryManager : MonoBehaviour
    {
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFail;
        
        public static DeliveryManager Instance { get; private set; }
        [SerializeField] private RecipeSOList _recipeSOList;
        private List<RecipeSO> _waitingRecipeSOList;

        private float spawnRecipeTimer;
        private float spawnRecipeTimerMax = 4.0f;
        private int waitingRecipeMax = 4;
        private int _successfulRecipesDelivered = 0;

        private void Awake()
        {
            Instance = this;
            _waitingRecipeSOList = new List<RecipeSO>();
        }

        private void Update()
        {
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer <= 0f)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                if (_waitingRecipeSOList.Count < waitingRecipeMax)
                {
                    var waitingRecipeSO = _recipeSOList.recipeSOList[Random.Range(0, _recipeSOList.recipeSOList.Count)];
                    _waitingRecipeSOList.Add(waitingRecipeSO);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (int index = 0; index < _waitingRecipeSOList.Count; index++)
            {
                var waitingRecipeSO = _waitingRecipeSOList[index];
                if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
                {
                    var plateContentMatchesRecipe = true;
                    // has same number of ingredients
                    foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                    {
                        var ingredientFound = false;
                        foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                        {
                            if (recipeKitchenObjectSO == plateKitchenObjectSO)
                            {
                                ingredientFound = true;
                                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                                _successfulRecipesDelivered++;
                                break;
                            }
                        }

                        if (!ingredientFound)
                        {
                            plateContentMatchesRecipe = false;
                        }
                    }

                    if (plateContentMatchesRecipe)
                    {
                        _waitingRecipeSOList.RemoveAt(index);
                        return;
                    }
                }
            }
            OnRecipeFail?.Invoke(this, EventArgs.Empty);
            Debug.LogError("Player didn't deliver the correct recipe!");
        }

        public List<RecipeSO> GetWaitingRecipeSOList()
        {
            return _waitingRecipeSOList;
        }

        public int GetSuccessfulRecipesDeliveredCount()
        {
            return _successfulRecipesDelivered;
        }
    }
}