using System;
using System.Collections.Generic;
using KitchenChaos.Data;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;
        private List<KitchenObjectSO> _kitchenObjectSOList;

        public event EventHandler<OnIngredientAddedEventArg> OnIngredientAdded;
        public class OnIngredientAddedEventArg : EventArgs
        {
            public KitchenObjectSO kitchenObjectSO;
        }

        private void Awake()
        {
            _kitchenObjectSOList = new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) 
        {
            if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }
            if (_kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }
            else
            {
                _kitchenObjectSOList.Add(kitchenObjectSO);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArg
                {
                    kitchenObjectSO = kitchenObjectSO
                });
                return true;
            }
        }

        public List<KitchenObjectSO> GetKitchenObjectSOList()
        {
            return _kitchenObjectSOList;
        }
    }
}

