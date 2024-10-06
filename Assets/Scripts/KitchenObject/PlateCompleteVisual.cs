using System;
using System.Collections.Generic;
using KitchenChaos.Data;
using UnityEngine;

namespace KitchenChaos.Feature.Interaction
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject _plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectsList;

        private void Start()
        {
            _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
            foreach (var kitchenObjectSOGameObject in _kitchenObjectSOGameObjectsList)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(false);
            }
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArg e)
        {
            foreach (var kitchenObjectSOGameObject in _kitchenObjectSOGameObjectsList)
            {
                if (kitchenObjectSOGameObject.KitchenObjectSO == e.kitchenObjectSO)
                {
                    kitchenObjectSOGameObject.gameObject.SetActive(true);
                }
            }
        }

        [Serializable]
        public struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO KitchenObjectSO;
            public GameObject gameObject;
        }
    }
}